using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.IEntity;
using static SteeringCS.behaviour.StaticBehaviours;


namespace SteeringCS.behaviour
{
    class EvadeBehaviour<TE> : SteeringBehaviour<TE> where TE : MovingEntity, IEvader
    {
        public EvadeBehaviour(TE me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            if (ME.Pursuer == null)
                return new Vector2D();

            var pursuer = ME.Pursuer;
            var pursuerPos = ME.Pursuer.Pos;
            Vector2D toPursuer = pursuerPos - ME.Pos;

            //the quotiënt between the distance and the maximum speeds will serve to the determine
            //how far into the future we look
            double lookAheadTime = toPursuer.Length() /
                                   (ME.MaxSpeed + pursuer.Velocity.Length());

            return Flee(pursuerPos + pursuer.Velocity * lookAheadTime, ME, ME.PanicDistanceSq());
        }
    }
}
