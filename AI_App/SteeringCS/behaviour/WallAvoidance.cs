using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class WallAvoidance : ISteeringBehaviour<IWallAvoider>
    {
        public double MAX_SEE_AHEAD = 15;
        public double MAX_AVOID_FORCE = 75;
        public Vector2D leftSensor;
        public Vector2D centerSensor;
        public Vector2D rightSensor;
        private Vector2D affectedSensor;
        public IWallAvoider ME { get; set; }

        public WallAvoidance(IWallAvoider me)
        {
            ME = me;
        }

        public Vector2D Calculate()
        {
            Vector2D avoidanceForce = new Vector2D(0, 0);
            
            centerSensor = ME.Pos + ME.Velocity.Normalize() * MAX_SEE_AHEAD;
            leftSensor = new Vector2D(ME.Pos.X + ((ME.Side.X - ME.Heading.X) * -MAX_SEE_AHEAD / 2), ME.Pos.Y + ((ME.Side.Y - ME.Heading.Y) * -MAX_SEE_AHEAD / 2));
            rightSensor = new Vector2D(ME.Pos.X + ((ME.Side.X - ME.Heading.X * -1) * MAX_SEE_AHEAD / 2), ME.Pos.Y + ((ME.Side.Y - ME.Heading.Y * -1) * MAX_SEE_AHEAD / 2));

            IWall mostThreatening = GetClosestWall();

            if (mostThreatening != null)
            {
                avoidanceForce = new Vector2D(ME.Pos.X - affectedSensor.X, ME.Pos.Y - affectedSensor.Y);
                avoidanceForce = avoidanceForce.Normalize();

                avoidanceForce = avoidanceForce * (MAX_AVOID_FORCE * ME.Velocity.Length());
            }
            else
            {
                avoidanceForce = avoidanceForce * 0;
            }

            return avoidanceForce * 0.5;
        }

        public IWall GetClosestWall()
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
                affectedSensor = leftSensor;
                return true;
            }
            if(SensorWithinWall(wall, centerSensor))
            {
                affectedSensor = leftSensor;
                return true;
            }
            return false;
        }

        private bool SensorWithinWall(IWall wall, Vector2D sensor)
        {
            Vector2D topLeft = new Vector2D(wall.Pos.X, wall.Pos.Y);
            Vector2D bottomRight = new Vector2D(wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);

            return 
                sensor.X >= topLeft.X &&
                sensor.X <= bottomRight.X &&
                sensor.Y >= topLeft.Y &&
                sensor.Y <= bottomRight.Y;
        }
    }
}
