
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class EngineSpecification
    {
        public Manufacturer Manufacturer { get; set; }
        public virtual string Name { get; set; }
        public virtual int CylinderCount { get; set; }
        public virtual Propulsion PropulsionType { get; set; }
    }
}
