using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    //nog niet geimplementeerd
    class WanderBehaviour<TM> : SteeringBehaviour<TM> where TM : MovingEntity, IWanderer
    {
        private Random _rnd = new Random();
        public WanderBehaviour(TM me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            var heading = ME.Heading;
            var dist = heading * ME.WanderDistance;
            var wanderTarget = (heading * ME.WanderRadius) + dist;


            var x = _rnd.NextDouble() * (10 + 10) - 10;
            var y = _rnd.NextDouble() * (10 + 10) - 10;
            var jitterDistance = new Vector2D(x,y).Normalize() * ME.WanderJitter;
            var newWanderTarget = wanderTarget + jitterDistance;
            var neededForce = newWanderTarget.Normalize() * ME.MaxForce;
            return (neededForce - ME.Velocity).Truncate(ME.MaxForce);


        }

    }
}
