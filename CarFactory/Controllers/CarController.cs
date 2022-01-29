using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using CarFactory.ExceptionFilter;
using CarFactory.Extensions;
using CarFactory.InputModels;
using CarFactory_Domain;
using CarFactory_Domain.Models.InputModels;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CarFactory.Controllers
{
    [ApiController]
    [TypeFilter(typeof(ResponseExceptionFilter))]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        public CarController(ICarFactory carFactory)
        {
            _carFactory = carFactory;
        }

        [HttpPost]
        public IActionResult Post(BuildCarInputModel carsSpecs)
        {
            IEnumerable<CarSpecification> wantedCars = carsSpecs.ToCarSpecification();
            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            IEnumerable<Car> cars = _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();

            BuildCarOutputModel response = new BuildCarOutputModel
            {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            };

            return Ok(new ResponseBase<BuildCarOutputModel>(response));
        }
    }
}
