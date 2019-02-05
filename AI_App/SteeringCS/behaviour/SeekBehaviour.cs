using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour : ISteeringBehaviour
    {
        private IMover _me;
        public IEntity Target { get; set; }

        public SeekBehaviour(IMover me, IEntity target)
        {
            _me = me;
            Target = target;
        }
        public Vector2D Calculate()
        {
            if (Target == null)
                return new Vector2D(0, 0);
            return Seek(Target.Pos, _me);

        }
    }
}
