using CarFactory_Assembly;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class AssemblerTests
    {
        [TestMethod]
        public void Assembler_AssembleCar()
        {
            ICarAssembler carAssemblerMock = new CarAssembler();

            Chassis chassisMock = new Chassis("Three Doors Sedan Sportcar", true);
            Engine engineMock = new Engine(new EngineBlock(50), "Gasoline V12");
            Interior interiorMock = new Interior();
            List<Wheel> wheels = new List<Wheel>() 
            { 
                new Wheel(){ Manufacturer = Manufacturer.Plandrover},
                new Wheel(){ Manufacturer = Manufacturer.Plandrover},
                new Wheel(){ Manufacturer = Manufacturer.Plandrover},
                new Wheel(){ Manufacturer = Manufacturer.Plandrover},
            };


            Car car = carAssemblerMock.AssembleCar(chassisMock, engineMock, interiorMock, wheels);

            car.Chassis.Description.Should().Be("Three Doors Sedan Sportcar");
            car.Engine.EngineSpecification.Should().Be("Gasoline V12");
            car.Wheels.Count().Should().Be(4);
        }
    }
}
