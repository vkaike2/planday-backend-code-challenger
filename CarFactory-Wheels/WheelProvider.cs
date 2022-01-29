using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Domain.Exceptions;
using CarFactory_Factory;
using CarFactory_Storage;
using Newtonsoft.Json;

namespace CarFactory_Wheels
{
    public class WheelProvider : IWheelProvider
    {
        private readonly IGetRubberQuery _getRubberQuery;

        public WheelProvider(IGetRubberQuery getRubberQuery)
        {
            _getRubberQuery = getRubberQuery;
        }

        public IEnumerable<Wheel> GetWheels()
        {
            IEnumerable<Part> rubber = _getRubberQuery.Get();

            return new[]
            {
                CreateWheel(ref rubber),
                CreateWheel(ref rubber),
                CreateWheel(ref rubber),
                CreateWheel(ref rubber)
            };
        }

        private Wheel CreateWheel(ref IEnumerable<Part> allRubber)
        {
            if(allRubber.Count() < 50)
            {
                throw new CarFactoryException("must have at least 50 rubber parts to create a wheel");
            }
   
            IEnumerable<Part> rubber = allRubber.Take(50);
            
            return new Wheel(){Manufacturer = rubber.First().Manufacturer};
        }
    }
}