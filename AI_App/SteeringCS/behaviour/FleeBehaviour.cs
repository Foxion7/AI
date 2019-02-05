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
    class FleeBehaviour : ISteeringBehaviour
    {
        private IMover _me;
        public IEntity Target { get; set; }
        public double PanicDistanceSq { get; set; }

        public FleeBehaviour(IMover me, IEntity target, double panicDistanceSq)
        {
            _me = me;
            Target = target;
            PanicDistanceSq = panicDistanceSq;
        }

        public Vector2D Calculate()
        {
            if (Target == null)
                return new Vector2D(0, 0);
            return Flee(Target.Pos, _me, PanicDistanceSq);
        }
    }
}
