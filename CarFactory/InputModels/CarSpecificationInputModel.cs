using CarFactory_Domain;
using CarFactory_Domain.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory.InputModels
{
    public class CarSpecificationInputModel
    {
        public CarPaintSpecificationInputModel Paint { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
        public SpeakerSpecificationInputModel[] DoorSpeakers { get; set; }
    }
}
