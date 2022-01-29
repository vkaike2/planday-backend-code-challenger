using CarFactory_Domain.Engine;
using CarFactory_Engine;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void Engine_GetEngine_Planborghini()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Engine engine = engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.Planborgini);

            engine.EngineSpecification.Should().Be("Gasoline V12");
        }

        [TestMethod]
        public void Engine_GetEngine_AstonPlanday()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Engine engine = engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.AstonPlanday);

            engine.EngineSpecification.Should().Be("Gasoline V12");
        }

        [TestMethod]
        public void Engine_GetEngine_PlandayMotorWorks()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Engine engine = engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.PlandayMotorWorks);

            engine.EngineSpecification.Should().Be("Diesel Straight 4");
        }

        [TestMethod]
        public void Engine_GetEngine_Plandrover()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Engine engine = engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.Plandrover);

            engine.EngineSpecification.Should().Be("Gasoline V8");
        }

        [TestMethod]
        public void Engine_GetEngine_PlanfaRomeo()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Engine engine = engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.PlanfaRomeo);

            engine.EngineSpecification.Should().Be("Gasoline V6");
        }

        [TestMethod]
        public void Engine_GetEngine_Volksday()
        {
            IEngineProvider engineProviderMock = this.ArrangeFirst();

            Action act = () => engineProviderMock.GetEngine(CarFactory_Domain.Manufacturer.Volksday);

            act.Should().Throw<Exception>().WithMessage("There is no specification for this manufacturer");
        }


        private IEngineProvider ArrangeFirst()
        {
            IStorageProvider storageProviderMock = new StorageProviderMock();
            IGetPistons getPistonsMock = new GetPistons();
            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();
            IGetEngineSpecificationQuery getEngineSpecificationMock = new GetEngineSpecificationQuery(storageProviderMock);
            return new EngineProvider(getPistonsMock, steelSubcontractorMock, getEngineSpecificationMock, null);
        }



    }
}
