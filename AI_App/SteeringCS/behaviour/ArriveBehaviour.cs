using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class ArriveBehaviour : SteeringBehaviour
    {
        public ArriveBehaviour(MovingEntity me) : base(me)
        {

        }

        public override Vector2D Calculate()
        {
            var me = ME as Creature;
            var desiredVelocity = me?.Target.Pos.Sub(me.Pos);
            var neededForce = desiredVelocity?.Sub(me?.Velocity);
            return neededForce ?? new Vector2D(0,0);
        }
    }
}
