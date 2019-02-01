using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class ArrivalBehaviour<TA> : SteeringBehaviour<TA> where TA : MovingEntity, IArriver
    {
        public ArrivalBehaviour(TA me) : base(me)
        {
        }
        
        public override Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D();
            return Arrive(ME.Target.Pos, ME, ME.SlowingRadius);
        }
    }
}
