using CarFactory.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory.InputModels
{
    public class CarPaintSpecificationInputModel
    {
        public PaintType Type { get; set; }
        public string BaseColor { get; set; }
        public string StripeColor { get; set; }
        public string DotColor { get; set; }
    }
}
