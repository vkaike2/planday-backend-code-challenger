using CarFactory_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Factory
{
    public class CarSpecification
    {
        public readonly PaintJob PaintJob;
        public readonly Manufacturer Manufacturer;
        public readonly IEnumerable<SpeakerSpecification> DoorSpeakers;
        public readonly IEnumerable<SpeakerSpecification> FrontWindowSpeakers;

        public CarSpecification(PaintJob paint, Manufacturer manufacturer, IEnumerable<SpeakerSpecification> doorSpeakers, IEnumerable<SpeakerSpecification> frontWindowSpeakers)
        {
            PaintJob = paint;
            Manufacturer = manufacturer;
            DoorSpeakers = doorSpeakers;
            FrontWindowSpeakers = frontWindowSpeakers;
        }

        public class SpeakerSpecification
        {
            public bool IsSubwoofer;
        }
    }
}
