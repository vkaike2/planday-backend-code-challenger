using CarFactory.Utilities;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Domain.Engine.EngineSpecifications;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CarFactory_Engine
{
    public class EngineProvider : IEngineProvider
    {
        private readonly IGetPistons _getPistons;
        private readonly ISteelSubcontractor _steelSubContractor;
        private int SteelInventory = 0;
        private readonly IGetEngineSpecificationQuery _getEngineSpecification;
        private readonly IMemoryCache _cache;

        private const string MEMORY_KEY = "Engines";

        public EngineProvider(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache
            )
        {
            _getPistons = getPistons;
            _steelSubContractor = steelSubContractor;
            _getEngineSpecification = getEngineSpecification;
            _cache = cache;
        }

        private List<EngineSpecification> TryGetEngineSpecificationFromCash()
        {
            List<EngineSpecification> specifications;
            if (_cache.TryGetValue(MEMORY_KEY, out specifications))
            {
                return specifications;
            }

            specifications = _getEngineSpecification.GetAll();

            _cache.Set(MEMORY_KEY, specifications);
            return specifications;
        }

        public Engine GetEngine(Manufacturer manufacturer)
        {
            EngineSpecification specification;
            if (_cache != null)
            {
                specification = this.TryGetEngineSpecificationFromCash().FirstOrDefault(e => e.Manufacturer == manufacturer);
            }
            else
            {
                specification = _getEngineSpecification.GetForManufacturer(manufacturer);
            }

            if (specification == null)
            {
                throw new CarFactoryException("There is no specification for this manufacturer");
            }

            EngineBlock engineBlock = MakeEngineBlock(specification.CylinderCount);

            Engine engine = new Engine(engineBlock, specification.Name);

            int pistons = _getPistons.Get(specification.CylinderCount);

            InstallPistons(engine, pistons);

            InstallFuelInjectors(engine, specification.PropulsionType);

            InstallSparkPlugs(engine);

            if (!engine.IsFinished)
                throw new InvalidOperationException("Cannot return an unfinished engine");

            return engine;
        }

        private EngineBlock MakeEngineBlock(int cylinders)
        {
            int requiredSteel = cylinders * EngineBlock.RequiredSteelPerCylinder;

            int steel = GetSteel(requiredSteel);

            EngineBlock engineBlock = new EngineBlock(steel);

            if (cylinders != engineBlock.CylinderCount)
                throw new InvalidOperationException("Engine block does not have the required amount of cylinders");

            return engineBlock;
        }


        private int GetSteel(int amount)
        {
            if (amount > SteelInventory)
            {
                int missingSteel = amount - SteelInventory;
                SteelInventory += _steelSubContractor.OrderSteel(missingSteel).Sum(sd => sd.Amount);
            }

            SteelInventory -= amount;

            return amount;
        }

        private void InstallFuelInjectors(Engine engine, Propulsion propulsionType)
        {
            //Do work
            SlowWorker.FakeWorkingForMillis(25 * engine.EngineBlock.CylinderCount);

            engine.PropulsionType = propulsionType;
        }

        private void InstallPistons(Engine engine, int pistons)
        {
            //Do work
            SlowWorker.FakeWorkingForMillis(25 * pistons);

            engine.PistonsCount = pistons;
        }

        private void InstallSparkPlugs(Engine engine)
        {
            if (!engine.PropulsionType.HasValue)
                throw new InvalidOperationException($"Propulsion type must be known before installing spark plugs");

            if (engine.PropulsionType.Value == Propulsion.Gasoline)
            {
                //Do work 
                SlowWorker.FakeWorkingForMillis(engine.EngineBlock.CylinderCount * 15);
                engine.HasSparkPlugs = true;
            }
        }


    }
}