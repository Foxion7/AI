using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.entity;
using SteeringCS.IEntity;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    class FlockBehaviour<TF> : SteeringBehaviour<TF> where TF: MovingEntity, IFlocker
    {
        public FlockBehaviour(TF me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            var group = ME.Neighbors;
            var groupL= group as List<MovingEntity> ?? group.ToList();
            if (!groupL.Any())
                return new Vector2D();

            var separationForce =  Separation(groupL, ME) * ME.SeparationValue;
            var alignmentForce = Alignment(groupL)        * ME.AlignmentValue;
            var cohesionForce = Cohesion(groupL, ME)      * ME.CohesionValue;
            return (separationForce + alignmentForce + cohesionForce).Truncate(ME.MaxForce);
        }
    }
}
