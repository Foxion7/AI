using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.IEntity;

namespace SteeringCS.behaviour
{
    class EvadeBehaviour<TE> : FleeBehaviour<TE> where TE : MovingEntity, IEvader
    {
        public EvadeBehaviour(TE me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            if (ME.Pursuer == null)
                return new Vector2D();

            /* Not necessary to include the check for facing direction this time */
            var pursuer = ME.Pursuer;
            var pursuerPos = ME.Pursuer.Pos;
            Vector2D toPursuer = pursuerPos - ME.Pos;

            //the look-ahead time is proportional to the distance between the pursuer
            //and the evader; and is inversely proportional to the sum of the
            //agents' velocities
            double lookAheadTime = toPursuer.Length() /
                                   (ME.MaxSpeed + pursuer.Velocity.Length());
            //now flee away from predicted future position of the pursuer
            return Flee(pursuerPos + pursuer.Velocity * lookAheadTime);
        }
    }
}
