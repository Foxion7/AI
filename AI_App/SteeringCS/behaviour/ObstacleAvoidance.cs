using SteeringCS.entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS.behaviour
{
    class ObstacleAvoidance : ISteeringBehaviour<IObstacleAvoider>
    {
        public double MAX_SEE_AHEAD = 15;
        public double MAX_AVOID_FORCE = 100;
        public Vector2D ahead;
        public Vector2D ahead2;
        public IObstacleAvoider ME { get; set; }

        public ObstacleAvoidance(IObstacleAvoider me)
        {
            ME = me;
        }

        public Vector2D Calculate()
        {
            Vector2D avoidanceForce = new Vector2D(0,0);
            
            double dynamic_length = ME.Velocity.Length() / ME.MaxSpeed;
            ahead = ME.Pos + ME.Velocity.Normalize() * dynamic_length;

            ahead2 = ME.Pos + ME.Velocity.Normalize() * MAX_SEE_AHEAD * 0.5;

            IObstacle mostThreatening = findMostThreateningObstacle();
            
            if (mostThreatening != null)
            {
                avoidanceForce = new Vector2D(ahead.X - mostThreatening.Center.X, ahead.Y - mostThreatening.Center.Y);
                avoidanceForce = avoidanceForce.Normalize();

                avoidanceForce = avoidanceForce * (MAX_AVOID_FORCE * ME.Velocity.Length());
            } else
            {
                avoidanceForce = avoidanceForce * 0;
            }
            return avoidanceForce;
        }

        private IObstacle findMostThreateningObstacle() {
            IObstacle mostThreatening = null;
            
            for (int i = 0; i<ME.Obstacles.Count(); i++) {
                IObstacle obstacle = ME.Obstacles[i];
                bool collision = lineIntersectsCircleAhead(obstacle);
 
                if (collision && (mostThreatening == null || VectorMath.DistanceBetweenPositions(ME.Pos, obstacle.Center) < VectorMath.DistanceBetweenPositions(ME.Pos, mostThreatening.Center))) {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool lineIntersectsCircleAhead(IObstacle obstacle)
        {
            return VectorMath.DistanceBetweenPositions(obstacle.Center, ahead) <= obstacle.Radius || VectorMath.DistanceBetweenPositions(obstacle.Center, ahead2) <= obstacle.Radius;
        }

        private double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
        }
    }
}
