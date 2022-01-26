using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CarFactory_Domain;
using CarFactory_Domain.Models.InputModels;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CarFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        public CarController(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public object Post([FromBody][Required] BuildCarInputModel carsSpecs)
        {

            IEnumerable<CarSpecification> wantedCars = TransformToDomainObjects(carsSpecs);
            string jsonObject = JsonConvert.SerializeObject(wantedCars);
            //Build cars
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IEnumerable<Car> cars = _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();

            //Create response and return
            BuildCarOutputModel response =new BuildCarOutputModel
            {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            };

            return response;
        }

        private static IEnumerable<CarSpecification> TransformToDomainObjects(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            List<CarSpecification> wantedCars = new List<CarSpecification>();
            foreach (BuildCarInputModelItem spec in carsSpecs.Cars)
            {
                for(var i = 1; i <= spec.Amount; i++)
                {
                    if(spec.Specification.NumberOfDoors % 2 == 0)
                    {
                        throw new ArgumentException("Must give an odd number of doors");
                    }
                    PaintJob? paint = null;
                    Color baseColor = Color.FromName(spec.Specification.Paint.BaseColor);
                    switch (spec.Specification.Paint.Type)
                    {
                        case "single":
                            paint = new SingleColorPaintJob(baseColor);
                            break;
                        case "stripe":
                            paint = new StripedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.StripeColor));
                            break;
                        case "dot":
                            paint = new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor));
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unknown paint type %", spec.Specification.Paint.Type));
                    }
                    IEnumerable<CarSpecification.SpeakerSpecification> dashboardSpeakers = spec.Specification.FrontWindowSpeakers.Select(s => new CarSpecification.SpeakerSpecification { IsSubwoofer = s.IsSubwoofer });
                    CarSpecification.SpeakerSpecification[] doorSpeakers = new CarSpecification.SpeakerSpecification[0]; //TODO: Let people install door speakers

                    CarSpecification wantedCar = new CarSpecification(paint, spec.Specification.Manufacturer, spec.Specification.NumberOfDoors, doorSpeakers, dashboardSpeakers);
                    wantedCars.Add(wantedCar);
                }
            }
            return wantedCars;
        }
    }
}
