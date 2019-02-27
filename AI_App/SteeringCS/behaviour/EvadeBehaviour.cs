using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;


namespace SteeringCS.behaviour
{
    class EvadeBehaviour : ISteeringBehaviour
    {
        private readonly IMover _me;
        public IMover Pursuer { get; set; }
        public double PanicDistanceSq { get; set; }

        public EvadeBehaviour(IMover me, IMover pursuer, double panicDistanceSq)
        {
            _me = me;
            Pursuer = pursuer;
            PanicDistanceSq = panicDistanceSq;
        }

        public Vector2D Calculate()
        {
            if (Pursuer == null)
                return new Vector2D();

            Vector2D toPursuer = Pursuer.Pos - _me.Pos;

            //the quotiënt between the distance and the maximum speeds will serve to the determine
            //how far into the future we look
            double lookAheadTime = toPursuer.Length() /
                                   (_me.MaxSpeed + Pursuer.Velocity.Length());

            return Flee(Pursuer.Pos + Pursuer.Velocity * lookAheadTime, _me, PanicDistanceSq);
        }
    }
}
