using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory.InputModels
{
    public class BuildCarInputModel
    {
        public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
    }
}
