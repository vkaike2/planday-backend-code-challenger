using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain.Models.InputModels
{
    public class BuildCarOutputModel
    {
        public long RunTime { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
