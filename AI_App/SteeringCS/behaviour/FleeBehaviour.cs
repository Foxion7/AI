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
    public class FleeBehaviour : ISteeringBehaviour
    {
        private IMover _me;
        public IEntity Target { get; set; }
        public double PanicDistance { get; set; }

        public FleeBehaviour(IMover me, IEntity target, double panicDistance)
        {
            _me = me;
            Target = target;
            PanicDistance = panicDistance;
        }

        public Vector2D Calculate()
        {
            if (Target == null)
                return new Vector2D(0, 0);
            return Flee(Target.Pos, _me, PanicDistance);
        }
    }
}
