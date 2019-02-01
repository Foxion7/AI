using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.IEntity
{
    public interface IFlocker : IGrouper<MovingEntity>
    {
        int SeparationValue { get; }
        int CohesionValue { get; }
        int AlignmentValue { get; }
    }
}
