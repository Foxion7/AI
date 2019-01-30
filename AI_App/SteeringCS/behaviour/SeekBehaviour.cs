using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class SeekBehaviour : SteeringBehaviour
    {
        public SeekBehaviour(MovingEntity me) : base(me){}

        public override Vector2D Calculate()
        {
            var me = ME as Creature;
            var desiredVelocity = me?.Target.Pos.Sub(me.Pos).Normalize().Multiply(me.MaxSpeed);
            var neededForce = desiredVelocity?.Sub(me?.Velocity);
            return neededForce ?? new Vector2D(0,0);
        }
    }
}
