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
            if(ME.WanderCircle == null)
                return new Vector2D();
            //first, add a small random vector to the target’s position (RandomClamped
            //returns a value between -1 and 1)
            var wndr = ME.WanderCircle;
            wndr.WanderTarget += new Vector2D(RandomClamped() * wndr.WanderJitter,
                RandomClamped() * wndr.WanderJitter);

            //reproject this new vector back onto a unit circle
            wndr.WanderTarget = wndr.WanderTarget.Normalize();
            wndr.WanderTarget *= wndr.WanderRadius;

            //move the target into a position WanderDist in front of the agent
            Vector2D targetLocal = wndr.WanderTarget + new Vector2D(wndr.WanderDistance, 0);
            //project the target into world space
            //Vector2D targetWorld = PointToWorldSpace(targetLocal,
            //    ME.Heading,
            //    ME.Side,
            //    ME.Pos);
            //and steer toward it
            return targetLocal - ME.Pos;

        }

        private double RandomClamped()
        {
            return (_rnd.NextDouble()*2)-1;
        }
    }
}
