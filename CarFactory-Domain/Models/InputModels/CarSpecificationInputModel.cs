using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain.Models.InputModels
{
    public class CarSpecificationInputModel
    {
        public int NumberOfDoors { get; set; }
        public CarPaintSpecificationInputModel Paint { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
    }
}
