using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Mocks;

namespace UnitTests
{
    [TestClass]
    public class WheelTests
    {

        [TestMethod]
        public void WheelProvider_GetWheels()
        {
            IStorageProvider storageProviderMock = new StorageProviderMock();
            IGetRubberQuery rubberyQueryMock = new GetRubberQuery(storageProviderMock);
            IWheelProvider wheelProviderMock = new WheelProvider(rubberyQueryMock);

            IEnumerable<CarFactory_Domain.Wheel> wheels = wheelProviderMock.GetWheels();

            wheels.Count().Should().Be(4);
        }

        [TestMethod]
        public void WheelProvider_WhithoutRubber()
        {
            StorageProviderMock storageProviderMock = new StorageProviderMock();
            storageProviderMock.DontInsertRubber = true;
            IGetRubberQuery rubberyQueryMock = new GetRubberQuery(storageProviderMock);
            IWheelProvider wheelProviderMock = new WheelProvider(rubberyQueryMock);

            Action act = () => wheelProviderMock.GetWheels();

            act.Should().Throw<Exception>().WithMessage("must have at least 50 rubber parts to create a wheel");
        }
    }
}
