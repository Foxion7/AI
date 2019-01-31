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
            // Calculate the desired velocity
            var desiredVelocity = targetPost - ME.Pos;
            var distance = desiredVelocity.Length();

            // Check the distance to detect whether the character
            // is inside the slowing area
            if (distance < ME.SlowingRadius)
            {
                // Inside the slowing area
                desiredVelocity = desiredVelocity.Truncate(ME.MaxSpeed) * (distance / ME.SlowingRadius);
            }
            else
            {
                // Outside the slowing area.
                desiredVelocity = desiredVelocity.Truncate(ME.MaxSpeed);
            }

            // Set the steering based on this
            return (desiredVelocity - ME.Velocity).Truncate(ME.MaxForce);
        }
    }
}
