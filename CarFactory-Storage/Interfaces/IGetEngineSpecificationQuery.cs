using CarFactory_Domain;
using CarFactory_Domain.Engine.EngineSpecifications;
using System.Collections.Generic;

namespace CarFactory_Storage
{
    public interface IGetEngineSpecificationQuery
    {
        EngineSpecification GetForManufacturer(Manufacturer manufacturer);
        List<EngineSpecification> GetAll();
    }
}
