using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain.Exceptions
{
    public class CarFactoryException : Exception
    {
        public CarFactoryException(string msg): base(msg)
        {

        }
    }
}
