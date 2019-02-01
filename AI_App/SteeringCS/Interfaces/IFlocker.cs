using SteeringCS.entity;

namespace SteeringCS.Interfaces
{
    public interface IFlocker : IGrouper<IMover>
    {
        int SeparationValue { get; }
        int CohesionValue { get; }
        int AlignmentValue { get; }
    }
}
