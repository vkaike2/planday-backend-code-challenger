using CarFactory_Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace CarFactory_Factory
{
    public class CarFactory : ICarFactory
    {
        private IChassisProvider _chassisProvider;
        private IEngineProvider _engineProvider;
        private IPainter _painter;
        private IInteriorProvider _interiorProvider;
        private IWheelProvider _wheelProvider;
        private ICarAssembler _carAssembler;
        private IStorageProvider _storageProvider;

        public CarFactory(
            IChassisProvider chassisProvider, 
            IEngineProvider engineProvider, 
            IPainter painter, 
            IInteriorProvider interiorProvider, 
            IWheelProvider wheelProvider, 
            ICarAssembler carAssembler,
            IStorageProvider storageProvider)
        {
            _chassisProvider = chassisProvider;
            _engineProvider = engineProvider;
            _painter = painter;
            _interiorProvider = interiorProvider;
            _wheelProvider = wheelProvider;
            _carAssembler = carAssembler;
            _storageProvider = storageProvider;
        }

        public IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        {
            List<Car> cars = new List<Car>();
            foreach(CarSpecification spec in specs)
            {
                Chassis chassis = _chassisProvider.GetChassis(spec.Manufacturer, spec.NumberOfDoors);
                CarFactory_Domain.Engine.Engine engine = _engineProvider.GetEngine(spec.Manufacturer);
                Interior interior = _interiorProvider.GetInterior(spec);
                IEnumerable<Wheel> wheels = _wheelProvider.GetWheels();
                Car car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);
                Car paintedCar = _painter.PaintCar(car, spec.PaintJob);
                cars.Add(paintedCar);
                ;
            }
            return cars;
        }
    }
}