using CarFactory_Domain;
using CarFactory_Domain.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

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

        public CarFactory(
            IChassisProvider chassisProvider,
            IEngineProvider engineProvider,
            IPainter painter,
            IInteriorProvider interiorProvider,
            IWheelProvider wheelProvider,
            ICarAssembler carAssembler)
        {
            _chassisProvider = chassisProvider;
            _engineProvider = engineProvider;
            _painter = painter;
            _interiorProvider = interiorProvider;
            _wheelProvider = wheelProvider;
            _carAssembler = carAssembler;
        }

        public  IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        {
            List<Car> cars = new List<Car>();





            Chassis chassis = null;
            Engine engine = null;
            Interior interior = null;
            IEnumerable<Wheel> wheels = null;
            Car car = null;

            Task chassisTask = null;
            Task engineTask = null;
            Task interiorTask = null;
            Task wheelsTask = null;

            foreach (CarSpecification spec in specs)
            {
                chassisTask = new Task(() =>
                {
                    chassis = _chassisProvider.GetChassis(spec.Manufacturer);
                });
                engineTask = new Task(() =>
                {
                    engine = _engineProvider.GetEngine(spec.Manufacturer);
                });
                interiorTask = new Task(() =>
                {
                    interior = _interiorProvider.GetInterior(spec);
                });
                wheelsTask = new Task(() =>
                {
                    wheels = _wheelProvider.GetWheels();
                });

                chassisTask.Start();
                engineTask.Start();
                interiorTask.Start();
                wheelsTask.Start();

                Task.WaitAll(chassisTask, engineTask, interiorTask, wheelsTask);


                car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);

                if (!spec.PaintJob.AreInstructionsUnlocked())
                {
                    car = _painter.PaintCar(car, spec.PaintJob);
                }
                else
                {
                    car.PaintJob = spec.PaintJob;
                }
                
                cars.Add(car);
            }
            
            
            return cars;
        }



    }
}