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
    public class ArrivalBehaviour : ISteeringBehaviour<IArriver>
    {
        public IArriver ME { get; set; }
        public ArrivalBehaviour(IArriver me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D();
            return Arrive(ME.Target.Pos, ME, ME.SlowingRadius);
        }
    }
}
