using CarFactory_Chasis;
using CarFactory_Domain;
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
    public class ChassisTest
    {

        [TestMethod]
        public void Chassis_GetChassis_Planborgini()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Chassis chassis = chassisProviderMock.GetChassis(Manufacturer.Planborgini);

            chassis.Description.Should().Be("Three Doors Sedan Sportcar");
        }

        [TestMethod]
        public void Chassis_GetChassis_Plandrover()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Chassis chassis = chassisProviderMock.GetChassis(Manufacturer.Plandrover);

            chassis.Description.Should().Be("Five Doors Hatchback Offroader");
        }

        [TestMethod]
        public void Chassis_GetChassis_PlanfaRomeo()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Chassis chassis = chassisProviderMock.GetChassis(Manufacturer.PlanfaRomeo);

            chassis.Description.Should().Be("Five Doors Sedan Family car");
        }

        [TestMethod]
        public void Chassis_GetChassis_PlandayMotorWorks()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Chassis chassis = chassisProviderMock.GetChassis(Manufacturer.PlandayMotorWorks);

            chassis.Description.Should().Be("Three Doors Pickup Offroader");
        }

        [TestMethod]
        public void Chassis_GetChassis_AstonPlanday()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Chassis chassis = chassisProviderMock.GetChassis(Manufacturer.AstonPlanday);

            chassis.Description.Should().Be("Five Doors Sedan Sportcar");
        }

        [TestMethod]
        public void Chassis_GetChassis_Volksday()
        {
            IChassisProvider chassisProviderMock = this.ArrangeFirst();

            Action act = () => chassisProviderMock.GetChassis(Manufacturer.Volksday);

            act.Should().Throw<Exception>().WithMessage("There is no chassis recipe for this manufacturer");
        }

        private IChassisProvider ArrangeFirst()
        {
            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();
            IGetChassisRecipeQuery getChassisRecipeQueryMock = new GetChassisRecipeQuery(new StorageProviderMock());
            return new ChassisProvider(steelSubcontractorMock, getChassisRecipeQueryMock,null);
        }
    }
}
