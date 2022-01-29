using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisProvider : IChassisProvider
    {
        private readonly ISteelSubcontractor _steelSubcontractor;
        private readonly IGetChassisRecipeQuery _chassisRecipeQuery;
        private readonly ChassisWelder _chassisWelder;
        private readonly IMemoryCache _cache;

        private const string MEMORY_KEY = "CHASSIS";

        public int SteelInventory { get; private set; }


        public ChassisProvider(
            ISteelSubcontractor steelSubcontractor,
            IGetChassisRecipeQuery chassisRecipeQuery,
            IMemoryCache memoryCache)
        {
            _steelSubcontractor = steelSubcontractor;
            _chassisRecipeQuery = chassisRecipeQuery;
            _chassisWelder = new ChassisWelder();
            _cache = memoryCache;
        }


        private List<ChassisRecipe> TryGetEngineChassisRecipeFromCash()
        {
            List<ChassisRecipe> chassisRecipe;
            if (_cache.TryGetValue(MEMORY_KEY, out chassisRecipe))
            {
                return chassisRecipe;
            }

            chassisRecipe = _chassisRecipeQuery.GetAll();

            _cache?.Set(MEMORY_KEY, chassisRecipe);
            return chassisRecipe;
        }

        public Chassis GetChassis(Manufacturer manufacturer)
        {
            ChassisRecipe chassisRecipe;
            if (_cache != null)
            {
                chassisRecipe = this.TryGetEngineChassisRecipeFromCash().FirstOrDefault(e => e.Manufacturer == manufacturer);
            }
            else
            {
                chassisRecipe = _chassisRecipeQuery.Get(manufacturer);
            }

            if (chassisRecipe == null)
            {
                throw new CarFactoryException("There is no chassis recipe for this manufacturer");
            }

            List<ChassisPart> chassisParts = new List<ChassisPart>();

            chassisParts.Add(new ChassisBack(chassisRecipe.BackId));
            chassisParts.Add(new ChassisCabin(chassisRecipe.CabinId));
            chassisParts.Add(new ChassisFront(chassisRecipe.FrontId));

            CheckChassisParts(chassisParts);
            SteelInventory += _steelSubcontractor.OrderSteel(chassisRecipe.Cost).Select(d => d.Amount).Sum();
            CheckForMaterials(chassisRecipe.Cost);

            SteelInventory -= chassisRecipe.Cost;


            return _chassisWelder.Weld(
                (ChassisBack)chassisParts[0],
                (ChassisCabin)chassisParts[1],
                (ChassisFront)chassisParts[2]);
        }


        private void CheckForMaterials(int cost)
        {
            if (SteelInventory < cost)
            {
                throw new CarFactoryException("Not enough chassis material");
            }
        }

        private void CheckChassisParts(List<ChassisPart> parts)
        {
            if (parts == null)
            {
                throw new CarFactoryException("No chassis parts");
            }
            else if (parts.Count > 3)
            {
                throw new CarFactoryException("Chassis parts missing");
            }
            else if (parts.Count < 3)
            {
                throw new CarFactoryException("To many chassis parts");
            }
        }
    }
}
