using CarFactory_Domain;
using CarFactory_Domain.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

        //public IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        //{
        //    List<Car> cars = new List<Car>();

        //    Chassis chassis = null;
        //    Engine engine = null;
        //    Interior interior = null;
        //    IEnumerable<Wheel> wheels = null;
        //    Car car = null;

        //    Task chassisTask = null;
        //    Task engineTask = null;
        //    Task interiorTask = null;
        //    Task wheelsTask = null;

        //    foreach (CarSpecification spec in specs)
        //    {
        //        chassisTask = new Task(() =>
        //        {
        //            chassis = _chassisProvider.GetChassis(spec.Manufacturer);
        //        });
        //        engineTask = new Task(() =>
        //        {
        //            engine = _engineProvider.GetEngine(spec.Manufacturer);
        //        });
        //        interiorTask = new Task(() =>
        //        {
        //            interior = _interiorProvider.GetInterior(spec);
        //        });
        //        wheelsTask = new Task(() =>
        //        {
        //            wheels = _wheelProvider.GetWheels();
        //        });

        //        chassisTask.Start();
        //        engineTask.Start();
        //        interiorTask.Start();
        //        wheelsTask.Start();

        //        Task.WaitAll(chassisTask, engineTask, interiorTask, wheelsTask);


        //        car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);

        //        if (!spec.PaintJob.AreInstructionsUnlocked())
        //        {
        //            car = _painter.PaintCar(car, spec.PaintJob);
        //        }
        //        else
        //        {
        //            car.PaintJob = spec.PaintJob;
        //        }

        //        cars.Add(car);
        //    }


        //    return cars;
        //}

        public  IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        {
            List<Car> cars = new List<Car>();

            //Spearating by brand
            IEnumerable<CarSpecification> specsPlanborgini = specs.Where(e => e.Manufacturer == Manufacturer.Planborgini).ToList();
            IEnumerable<CarSpecification> specsPlandayMotorWorks = specs.Where(e => e.Manufacturer == Manufacturer.PlandayMotorWorks).ToList();
            IEnumerable<CarSpecification> specsPlanfaRomeo = specs.Where(e => e.Manufacturer == Manufacturer.PlanfaRomeo).ToList();
            IEnumerable<CarSpecification> specsAstonPlanday = specs.Where(e => e.Manufacturer == Manufacturer.AstonPlanday).ToList();
            IEnumerable<CarSpecification> specsPlandrover = specs.Where(e => e.Manufacturer == Manufacturer.Plandrover).ToList();
            IEnumerable<CarSpecification> specsVolksday = specs.Where(e => e.Manufacturer == Manufacturer.Volksday).ToList();

            List<Task> taskList = new List<Task>();

            //creating tasks
            if (specsPlanborgini.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsPlanborgini))));
            }

            if (specsPlandayMotorWorks.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsPlandayMotorWorks))));
            }

            if (specsPlanfaRomeo.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsPlanfaRomeo))));
            }

            if (specsAstonPlanday.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsAstonPlanday))));
            }

            if (specsPlandrover.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsPlandrover))));
            }

            if (specsVolksday.Any())
            {
                taskList.Add(new Task(() => cars.AddRange(BuildMultipleCars(specsVolksday))));
            }

            // running every tak
            foreach (Task task in taskList)
            {
                task.Start();
            }

            // waiting for every task
            Task.WaitAll(taskList.ToArray());



            return cars;
        }

        public IEnumerable<Car> BuildMultipleCars(IEnumerable<CarSpecification> specs)
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


        Car BuildSingleCar(CarSpecification spec)
        {
            Chassis chassis = null;
            Engine engine = null;
            Interior interior = null;
            IEnumerable<Wheel> wheels = null;
            Car car = null;

            Task chassisTask = null;
            Task engineTask = null;
            Task interiorTask = null;
            Task wheelsTask = null;


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

            return car;
        }
    }
}