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
    public class ObstacleAvoidance : ISteeringBehaviour
    {
        private IObstacleAvoider _me;
        public double MaxSeeAhead { get; set; }
        public double MaxAvoidForce { get; set; }
        private Vector2D _ahead;
        private Vector2D _ahead2;

        public ObstacleAvoidance(IObstacleAvoider me, double maxSeeAhead = 15, double maxAvoidForce = 100)
        {
            _me = me;
            MaxSeeAhead = maxSeeAhead;
            MaxAvoidForce = maxAvoidForce;
        }


        public Vector2D Calculate()
        {
            Vector2D avoidanceForce = new Vector2D(0,0);
            
            _ahead = _me.Pos + _me.Velocity.Normalize() * MaxSeeAhead;
            _ahead2 = _me.Pos + _me.Velocity.Normalize() * MaxSeeAhead * 0.5;
            IObstacle mostThreatening = findMostThreateningObstacle(_me);
            
            if (mostThreatening != null)
            {
                avoidanceForce = new Vector2D(_ahead.X - mostThreatening.Center.X, _ahead.Y - mostThreatening.Center.Y);
                avoidanceForce = avoidanceForce.Normalize();

                avoidanceForce = avoidanceForce * (MaxAvoidForce * _me.Velocity.Length());
            } else
            {
                avoidanceForce = avoidanceForce * 0;
            }
            return avoidanceForce * 0.5;
        }

        private IObstacle findMostThreateningObstacle(IObstacleAvoider me) {
            IObstacle mostThreatening = null;
            
            for (int i = 0; i<me.Obstacles.Count(); i++) {
                IObstacle obstacle = me.Obstacles[i];
                bool collision = lineIntersectsCircleAhead(obstacle);
 
                if (collision && (mostThreatening == null || VectorMath.DistanceBetweenPositions(me.Pos, obstacle.Center) < VectorMath.DistanceBetweenPositions(me.Pos, mostThreatening.Center))) {
                    mostThreatening = obstacle;
                }
            }
            return mostThreatening;
        }

        private bool lineIntersectsCircleAhead(IObstacle obstacle)
        {
            return VectorMath.DistanceBetweenPositions(obstacle.Center, _ahead) <= obstacle.Radius || VectorMath.DistanceBetweenPositions(obstacle.Center, _ahead2) <= obstacle.Radius;
        }
    }
}
