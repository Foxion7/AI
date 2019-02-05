using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class FlockBehaviour : ISteeringBehaviour 
    {
        private IGrouper _me;
        public double GroupValue { get; set; }
        public double CohesionValue { get; set; }
        public double AlignmentValue { get; set; }
        public double SeparationValue { get; set; }
        public FlockBehaviour(IGrouper me, double groupValue, double cohesionValue,
            double alignmentValue, double separationValue)
        {
            GroupValue = groupValue;
            CohesionValue = cohesionValue;
            AlignmentValue = alignmentValue;
            SeparationValue = separationValue;
            _me = me;
        }

        public Vector2D Calculate()
        {
            var group = _me.Neighbors;
            var groupL = group as List<IGrouper> ?? group.ToList();
            if (groupL.Count <= 1)
                return new Vector2D();


            var separationForce =  Separation(groupL, _me) * SeparationValue;
            var alignmentForce = Alignment(groupL)        * AlignmentValue;
            var cohesionForce = Cohesion(groupL, _me)      * CohesionValue;
            var desiredSpeed = (separationForce + alignmentForce + cohesionForce).Normalize()*GroupValue;
            var neededForce = desiredSpeed - _me.Velocity;
            return neededForce.Truncate(_me.MaxForce);
        }
    }
}
