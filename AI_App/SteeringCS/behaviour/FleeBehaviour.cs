using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    class FleeBehaviour<TF> : SteeringBehaviour<TF> where TF: MovingEntity, IFleer
    {
        public FleeBehaviour(TF me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D(0, 0);
            return Flee(ME.Target.Pos, ME, ME.PanicDistance);
        }
    }
}
