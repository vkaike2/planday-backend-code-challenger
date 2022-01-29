using CarFactory_SubContractor;
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
    public class SteelTests
    {

        [TestMethod]
        public void SteelTests_Order1Steel()
        {
            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();

            List<SteelDelivery> steels = steelSubcontractorMock.OrderSteel(1);

            steels.Count().Should().Be(1);
        }

        [TestMethod]
        public void SteelTests_Order50Steel()
        {
            ISteelSubcontractor steelSubcontractorMock =  new SteelSubcontractor();

            List<SteelDelivery> steels = steelSubcontractorMock.OrderSteel(50);

            steels.Count().Should().Be(1);
        }

        [TestMethod]
        public void SteelTests_Order60Steel()
        {
            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();

            List<SteelDelivery> steels = steelSubcontractorMock.OrderSteel(60);

            steels.Count().Should().Be(2);
        }

        [TestMethod]
        public void SteelTests_Order100Steel()
        {
            ISteelSubcontractor steelSubcontractorMock = new SteelSubcontractor();

            List<SteelDelivery> steels = steelSubcontractorMock.OrderSteel(100);

            steels.Count().Should().Be(2);
        }
    }
}
