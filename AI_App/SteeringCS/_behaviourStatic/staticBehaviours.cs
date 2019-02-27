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

        public static Vector2D Flee(Vector2D targetPos, IMover me, double panicDistance)
        {
            var distance = me.Pos - targetPos;

            if (VectorMath.DistanceBetweenPositions(me.Pos, targetPos) > panicDistance || distance.LenghtIsZero())
            {
                return new Vector2D(0, 0);
            }
            var desiredVelocity = distance.Normalize() * me.MaxSpeed;
            var neededForce = desiredVelocity - me.Velocity;
            return neededForce.Truncate(me.MaxForce);
        }

        public static Vector2D Arrive(Vector2D targetPos, IMover me, double slowingRadius)
        {
            Vector2D toTarget = targetPos - me.Pos;
            if (toTarget.LengthSquared() < 0.1)
                return new Vector2D();

            //calculate the distance to the target position
            // Calculate the desired velocity
            var desiredVelocity = targetPos - me.Pos;
            var distance = desiredVelocity.Length();

            // Slows when in slowingradius. Stops if in direct contact with target.
            if (distance < slowingRadius)
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
                if(!toAgent.LenghtIsZero())
                    steeringForce += toAgent.Normalize() / toAgent.Length();
                return steeringForce;
            });
            if (force.LenghtIsZero())
                return force;
            return force.Normalize();
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

            if (count == 0 || sum.LenghtIsZero())
                return new Vector2D();
            return (sum / count).Normalize();
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

            var force = Seek(centerOfMass / count, me);
            if (force.LenghtIsZero())
                return force;
            return Seek(centerOfMass / count, me).Normalize();
        }

        public static Vector2D Follow(Vector2D targetPos, IGrouper me, double slowingRadius)
        {
            var desired = Arrive(targetPos, me, slowingRadius);
            var distance = (targetPos - me.Pos);
            
            //if it is in the slowingRadius the vector is probably a slowing one so we want to keep it as is.
            if (distance.LengthSquared() < slowingRadius * slowingRadius)
                return desired;
            
            if (desired.LenghtIsZero())
                return desired;

            //only keep the direction;
            return desired.Normalize();
        }
    }
}
