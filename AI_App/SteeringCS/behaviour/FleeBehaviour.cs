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
    class FleeBehaviour : ISteeringBehaviour<IFleer> 
    {
        public IFleer ME { get; set; }
        public FleeBehaviour(IFleer me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D(0, 0);
            return Flee(ME.Target.Pos, ME, ME.PanicDistance);
        }
    }
}
