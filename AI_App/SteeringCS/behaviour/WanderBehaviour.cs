using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.behaviour
{
    public class WanderBehaviour: ISteeringBehaviour
    {
        private static Random _rnd = new Random();
        private IMover _me;
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }

        public WanderBehaviour(IMover me, double wanderRadius, double wanderDistance)
        {
            _me = me;
            WanderRadius = wanderRadius;
            WanderDistance = wanderDistance;
        }


        public Vector2D Calculate()
        {
            var heading = _me.Heading;
            var dist = heading * WanderDistance;
            
            var x = _rnd.NextDouble() * (10 + 10) - 10;
            var y = _rnd.NextDouble() * (10 + 10) - 10;
            Vector2D jitterDistance;
            if (Math.Abs(x) > 0.01 || Math.Abs(y) > 0.01)
            {
                jitterDistance = new Vector2D(x, y).Normalize() * WanderRadius;
            }
            else
            {
                jitterDistance = new Vector2D(x+0.1, y+0.1)* WanderRadius;
            }

            var wanderTarget = (dist + jitterDistance) * _me.MaxForce;
            return (wanderTarget - _me.Velocity).Truncate(_me.MaxForce);
        }
    }
}
