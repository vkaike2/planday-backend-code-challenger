using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Builders;
using CarFactory_Interior.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CarFactory_Factory.CarSpecification;

namespace UnitTests
{
    [TestClass]
    public class InteriorTests
    {

        [TestMethod]
        public void Interior_Getinterior_Ok()
        {
            IDashboardBuilder dashboardBuilderMock = new DashboardBuilder();
            ISeatBuilder seatBuilderMock = new SeatBuilder();
            ISpeakerBuilder speakerBuilderMock = new SpeakerBuilder();
            IInteriorProvider interiorProviderMock = new InteriorProvider(dashboardBuilderMock, seatBuilderMock, speakerBuilderMock);
            
            List<SpeakerSpecification> speakers = new List<SpeakerSpecification>(){
                new SpeakerSpecification(){ IsSubwoofer = true},
                new SpeakerSpecification(){ IsSubwoofer = true}
            };
            CarSpecification carSpecification = new CarSpecification(
                null,                                       // PaintJob
                CarFactory_Domain.Manufacturer.Planborgini,// Manufacturer
                speakers,                                   // FrontWindowSpeaker
                speakers                                    // DoorSpeaker
                );

            Interior interiorMock = interiorProviderMock.GetInterior(carSpecification);

            interiorMock.DoorSpeakers.Count().Should().Be(2);
            interiorMock.FrontWindowSpeakers.Count().Should().Be(2);
            interiorMock.Seats.Count().Should().Be(4);
            interiorMock.Dashboard.PartType.Should().Be(PartType.Wood);
        }

        [TestMethod]
        public void Interior_GetInterior_MoreThan2FrontSpeaker()
        {
            IDashboardBuilder dashboardBuilderMock = new DashboardBuilder();
            ISeatBuilder seatBuilderMock = new SeatBuilder();
            ISpeakerBuilder speakerBuilderMock = new SpeakerBuilder();
            IInteriorProvider interiorProviderMock = new InteriorProvider(dashboardBuilderMock, seatBuilderMock, speakerBuilderMock);
            List<SpeakerSpecification> speakers = new List<SpeakerSpecification>(){
                new SpeakerSpecification(){ IsSubwoofer = true},
                new SpeakerSpecification(){ IsSubwoofer = true},
                new SpeakerSpecification(){ IsSubwoofer = true}
            };
            CarSpecification carSpecification = new CarSpecification(
                null,                                       // PaintJob
                CarFactory_Domain.Manufacturer.Planborgini,// Manufacturer
                speakers,                                   // FrontWindowSpeaker
                speakers                                    // DoorSpeaker
                );

            Action act = () => interiorProviderMock.GetInterior(carSpecification);

            act.Should().Throw<ArgumentException>().WithMessage("More than 2 speakers aren't supported");
        }
    }
}
