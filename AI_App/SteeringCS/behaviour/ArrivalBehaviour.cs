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
    public class ArrivalBehaviour : ISteeringBehaviour
    {
        private readonly IMover _me;
        public IEntity Target { get; set; }
        public double  SlowingRadius { get; set; }

        public ArrivalBehaviour(IMover me, IEntity target, double slowingRadius)
        {
            _me = me;
            SlowingRadius = slowingRadius;
            Target = target;
        }

        public Vector2D Calculate()
        {
            if (Target == null)
                return new Vector2D();
            return Arrive(Target.Pos, _me, SlowingRadius);
        }
    }
}
