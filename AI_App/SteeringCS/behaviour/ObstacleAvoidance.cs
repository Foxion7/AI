using SteeringCS.entity;
using SteeringCS.IEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class ObstacleAvoidance<OA> : SteeringBehaviour<OA> where OA : MovingEntity, IObstacleAvoidance
    {
        public double MAX_SEE_AHEAD = 50;
        public double MAX_AVOID_FORCE = 250;
        public Vector2D ahead;
        public Vector2D ahead2;

        public ObstacleAvoidance(OA me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            Vector2D avoidanceForce = new Vector2D(0,0);

            //ahead = ME.Pos + ME.Velocity.Normalize() * MAX_SEE_AHEAD;

            double dynamic_length = ME.Velocity.Length() / ME.MaxSpeed;
            ahead = ME.Pos + ME.Velocity.Normalize() * dynamic_length;

            ahead2 = ME.Pos + ME.Velocity.Normalize() * MAX_SEE_AHEAD * 0.5;

            Obstacle mostThreatening = findMostThreateningObstacle();
            
            if (mostThreatening != null)
            {
                avoidanceForce = new Vector2D(ahead.X - mostThreatening.Center.X, ahead.Y - mostThreatening.Center.Y);
                avoidanceForce = avoidanceForce.Normalize();

                avoidanceForce = avoidanceForce * MAX_AVOID_FORCE;
            } else
            {
                avoidanceForce = avoidanceForce * 0;
            }
            return avoidanceForce;
        }

        private Obstacle findMostThreateningObstacle() {
            Obstacle mostThreatening = null;
 
            for (int i = 0; i<ME.MyWorld.obstacles.Count(); i++) {
                Obstacle obstacle = ME.MyWorld.obstacles[i];
                Boolean collision = lineIntersectsCircleAhead(obstacle);
 
                if (collision && (mostThreatening == null || DistanceBetweenPositions(ME.Pos, obstacle.Center) < DistanceBetweenPositions(ME.Pos, mostThreatening.Center))) {
                    mostThreatening = obstacle;
                    Console.WriteLine("Collision found with " + obstacle.name);
                }
            }
            return mostThreatening;
        }

        private bool lineIntersectsCircleAhead(Obstacle obstacle)
        {
            return DistanceBetweenPositions(obstacle.Center, ahead) <= obstacle.Radius + ME.Velocity.Length() || DistanceBetweenPositions(obstacle.Center, ahead2) <= obstacle.Radius + ME.Velocity.Length();
        }

        private double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
        }
    }
}
