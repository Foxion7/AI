using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.behaviour
{
    //nog niet geimplementeerd
    class WanderBehaviour: ISteeringBehaviour<IWanderer>
    {
        public IWanderer ME { get; set; }
        private Random _rnd = new Random();
        public WanderBehaviour(IWanderer me)
        {
            ME = me;
        }

        public Vector2D Calculate()
        {
            var heading = ME.Heading;
            var dist = heading * ME.WanderDistance;
            var wanderTarget = (heading * ME.WanderRadius) + dist;
            
            var x = _rnd.NextDouble() * (10 + 10) - 10;
            var y = _rnd.NextDouble() * (10 + 10) - 10;
            Vector2D jitterDistance;
            if (Math.Abs(x) > 0.01 && Math.Abs(y) > 0.01)
            {
                jitterDistance = new Vector2D(x, y).Normalize() * ME.WanderJitter;
            }
            else
            {
                jitterDistance = new Vector2D(x+0.1, y+0.1);
            }

            var newWanderTarget = wanderTarget + jitterDistance;;
            return (newWanderTarget - ME.Velocity).Truncate(ME.MaxForce);
        }

    }
}
