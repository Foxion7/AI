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
        public enum Deceleration { slow = 3, normal = 2, fast = 1 };
        private Deceleration deceleration;

        public ArriveBehaviour(MovingEntity me, Deceleration deceleration) : base(me)
        {
            this.deceleration = deceleration;
        }

        
        public override Vector2D Calculate()
        {
            var me = ME as Creature;

            Vector2D ToTarget = me?.Target.Pos.Sub(ME.Pos);

            //calculate the distance to the target position
            double dist = ToTarget.Length();
            
            if (dist > 0)
            {
                const double DecelerationTweaker = 1;
                
                double speed = dist / ((double)deceleration * DecelerationTweaker);
                
                speed = LimitToMaxSpeed(speed, me.MaxSpeed);
                Vector2D DesiredVelocity = ToTarget.Multiply(speed).Divide(dist);
                me.Velocity = me.Velocity.Multiply(0);

                return (DesiredVelocity.Sub(me.Velocity));
            }
            me.Velocity = me.Velocity.Multiply(0);

            return new Vector2D(0, 0);
        }

        private double LimitToMaxSpeed (double current, double max)
        {
            if (current > max)
            {
                return max;
            }
            return current;
        }
    }
}
