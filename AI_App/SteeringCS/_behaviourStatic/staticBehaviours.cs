using System;
using System.Collections.Generic;
using System.Linq;
using SteeringCS.Interfaces;

namespace SteeringCS.behaviour
{
    public static class StaticBehaviours
    {
        public static Vector2D Seek(Vector2D targetPos, IMover me)
        {
            var toTarget = targetPos - me.Pos;
            if (toTarget.LengthSquared() < 0.1)
                return new Vector2D();
            var desiredVelocity = (targetPos - me.Pos).Normalize() * me.MaxSpeed;
            var neededForce = desiredVelocity - me.Velocity;
            return neededForce.Truncate(me.MaxForce);
        }

        public static Vector2D Flee(Vector2D targetPos, IMover me, double panicDistanceSq)
        {
            var distance = (me.Pos - targetPos);
            if (panicDistanceSq < distance.LengthSquared() || distance.LengthSquared() < 0.1)
            {
                return new Vector2D(0, 0);
            };
            var desiredVelocity = distance.Normalize() * me.MaxSpeed;
            var neededForce = desiredVelocity - me.Velocity;
            return neededForce.Truncate(me.MaxForce);
        }

        public static Vector2D Arrive(Vector2D targetPost, IMover me, double slowingRadius)
        {

            Vector2D toTarget = targetPost - me.Pos;
            if (toTarget.LengthSquared() < 0.1)
                return new Vector2D();

            //calculate the distance to the target position
            // Calculate the desired velocity
            var desiredVelocity = targetPost - me.Pos;
            var distance = desiredVelocity.Length();

            // Slows when in slowingradius. Stops if in direct contact with target.
            if (distance < 15)
            {
                desiredVelocity = new Vector2D(0, 0);
            }
            else  if (distance < slowingRadius)
            {
                desiredVelocity = desiredVelocity.Truncate(me.MaxSpeed) * (distance / slowingRadius);
            }
            else 
            {
                // Outside the slowing area.
                desiredVelocity = desiredVelocity.Truncate(me.MaxSpeed);
            }

            // Set the steering based on this
            return (desiredVelocity - me.Velocity).Truncate(me.MaxForce);
        }

        public static Vector2D Separation(IEnumerable<IEntity> neighbors, IMover me)
        {
            var force = neighbors.Aggregate(new Vector2D(), (steeringForce, neighbor) =>
            {
                Vector2D toAgent = me.Pos - neighbor.Pos;
                if(Math.Abs(toAgent.LengthSquared()) > 0.1)
                    steeringForce += toAgent.Normalize() / toAgent.Length();
                return steeringForce;
            });

            return force;
        }

        public static Vector2D Alignment(IEnumerable<IMover> neighbors)
        {
            Vector2D sum = new Vector2D();
            var count = 0;
            foreach (var neighbor in neighbors)
            {
                sum += neighbor.Heading;
                count++;
            }
            return sum / count;
        }

        public static Vector2D Cohesion(IEnumerable<IEntity> neighbors, IMover me)
        {
            Vector2D centerOfMass = new Vector2D();
            var count = 0;
            foreach (var neighbor in neighbors)
            {
                centerOfMass += neighbor.Pos;
                count++;
            }
            return Seek(centerOfMass / count, me);
        }
    }
}
