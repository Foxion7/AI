using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    public class ArrivalBehaviour<TA> : SteeringBehaviour<TA> where TA : MovingEntity, IArriver
    {
        public ArrivalBehaviour(TA me) : base(me)
        {
        }
        
        public override Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D();
            return Arrive(ME.Target.Pos);

        }

        public Vector2D Arrive(Vector2D targetPost)
        {
            Vector2D toTarget = targetPost - ME.Pos;

            //calculate the distance to the target position
            double dist = toTarget.Length();
            var decelerationTweaker = ME.DecelerationTweaker;
            double velocityTweaker = ME.VelocityTweaker;

            ME.Velocity = ME.Velocity * velocityTweaker;


            double speed = dist / ((double)ME.Deceleration * decelerationTweaker);

            speed = LimitToMaxSpeed(speed, ME.MaxSpeed);
            Vector2D desiredVelocity = toTarget * (speed) / (dist);

            return (desiredVelocity - ME.Velocity).Truncate(ME.MaxForce);
        }

        private double LimitToMaxSpeed(double current, double max)
        {
            if (current > max)
            {
                return max;
            }
            return current;
        }

    }
}
