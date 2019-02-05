using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    public class WallAvoidance : ISteeringBehaviour
    {
        public double MaxSeeAhead { get; set; }
        public double MaxAvoidForce { get; set; }
        private Vector2D leftSensor;
        private Vector2D centerSensor;
        private Vector2D rightSensor;
        private Vector2D affectedSensor;
        private IWallAvoider _me;

        public WallAvoidance(IWallAvoider me, double maxSeeAhead = 15, double maxAvoidForce = 75)
        {
            _me = me;
            MaxSeeAhead = maxSeeAhead;
            MaxAvoidForce = maxAvoidForce;
        }

        public Vector2D Calculate()
        {
            Vector2D avoidanceForce = new Vector2D(0, 0);
            
            centerSensor = _me.Pos + _me.Velocity.Normalize() * MaxSeeAhead;
            leftSensor = new Vector2D(_me.Pos.X + ((_me.Side.X - _me.Heading.X) * -MaxSeeAhead / 2), _me.Pos.Y + ((_me.Side.Y - _me.Heading.Y) * -MaxSeeAhead / 2));
            rightSensor = new Vector2D(_me.Pos.X + ((_me.Side.X - _me.Heading.X * -1) * MaxSeeAhead / 2), _me.Pos.Y + ((_me.Side.Y - _me.Heading.Y * -1) * MaxSeeAhead / 2));

            IWall mostThreatening = GetClosestWall(_me);

            if (mostThreatening != null)
            {
                avoidanceForce = new Vector2D(_me.Pos.X - affectedSensor.X, _me.Pos.Y - affectedSensor.Y);
                avoidanceForce = avoidanceForce.Normalize();

                avoidanceForce = avoidanceForce * (MaxAvoidForce * _me.Velocity.Length());
            }
            else
            {
                avoidanceForce = avoidanceForce * 0;
            }

            return avoidanceForce * 0.5;
        }

        public IWall GetClosestWall(IWallAvoider ME)
        {
            IWall mostThreatening = null;

            for (int i = 0; i < ME.Walls.Count(); i++)
            {
                IWall wall = ME.Walls[i];
                bool collision = findSensorCollision(wall);

                if (collision && (mostThreatening == null || VectorMath.DistanceBetweenPositions(ME.Pos, wall.Center) < VectorMath.DistanceBetweenPositions(ME.Pos, mostThreatening.Center)))
                {
                    mostThreatening = wall;
                }
            }
            return mostThreatening;
        }

        private bool findSensorCollision(IWall wall)
        {
            if(SensorWithinWall(wall, leftSensor))
            {
                affectedSensor = leftSensor;
                return true;
            }
            if(SensorWithinWall(wall, rightSensor))
            {
                affectedSensor = rightSensor;
                return true;
            }
            if(SensorWithinWall(wall, centerSensor))
            {
                affectedSensor = centerSensor;
                return true;
            }
            return false;
        }

        private bool SensorWithinWall(IWall wall, Vector2D sensor)
        {
            Vector2D topLeft = new Vector2D(wall.Pos.X, wall.Pos.Y);
            Vector2D bottomRight = new Vector2D(wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);

            return 
                sensor.X >= topLeft.X     &&
                sensor.X <= bottomRight.X &&
                sensor.Y >= topLeft.Y     &&
                sensor.Y <= bottomRight.Y;
        }
    }
}
