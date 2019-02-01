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
    class EvadeBehaviour : ISteeringBehaviour<IEvader>
    {
        public IEvader ME { get; set; }
        public EvadeBehaviour(IEvader me)
        {
            ME = me;
        }


        public Vector2D Calculate()
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
