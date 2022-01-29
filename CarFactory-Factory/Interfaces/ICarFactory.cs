using CarFactory_Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFactory_Factory
{
    public interface ICarFactory
    {
        IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs);
    }
}