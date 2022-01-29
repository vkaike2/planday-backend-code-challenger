using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace UnitTests
{
    [TestClass]
    public class PainterTests
    {
        [TestMethod]
        public void Painter_WithoutChassis()
        {
            Painter painterMock = new Painter();
            SingleColorPaintJob painterJobMock = new SingleColorPaintJob(Color.Aqua);
            Car mockCar = new Car(null, new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);

            Action act = () => painterMock.PaintCar(mockCar, painterJobMock);

            act.Should().Throw<Exception>().WithMessage("Cannot paint a car without chassis");
        }


        [TestMethod]
        public void Painter_SingleColor()
        {
            Painter painterMock = new Painter();
            SingleColorPaintJob painterJobMock = new SingleColorPaintJob(Color.Aqua);
            Car mockCar = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);

            Car paintedCar = painterMock.PaintCar(mockCar, painterJobMock);
            PaintJob job = paintedCar.PaintJob;

            job.BaseColor.Should().Be(Color.Aqua);
        }

        [TestMethod]
        public void Painter_StripedColor()
        {
            Painter painterMock = new Painter();
            StripedPaintJob painterJobMock = new StripedPaintJob(Color.Blue,Color.White);
            Car mockCar = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);

            Car paintedCar = painterMock.PaintCar(mockCar, painterJobMock);
            PaintJob job = paintedCar.PaintJob;

            job.BaseColor.Should().Be(Color.Blue);
            job.StripeColor.Should().Be(Color.White);
        }

        [TestMethod]
        public void Painter_DottedColor()
        {
            Painter painterMock = new Painter();
            DottedPaintJob painterJobMock = new DottedPaintJob(Color.Blue, Color.White);
            Car mockCar = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);

            Car paintedCar = painterMock.PaintCar(mockCar, painterJobMock);
            PaintJob job = paintedCar.PaintJob;

            job.BaseColor.Should().Be(Color.Blue);
            job.DotColor.Should().Be(Color.White);
        }
    }
}
