using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain.Models.InputModels
{
    public class CarPaintSpecificationInputModel
    {
        public string Type { get; set; }
        public string BaseColor { get; set; }
        public string? StripeColor { get; set; }
        public string? DotColor { get; set; }
    }
}
