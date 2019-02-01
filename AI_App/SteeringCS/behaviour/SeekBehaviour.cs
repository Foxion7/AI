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
    public class SeekBehaviour : ISteeringBehaviour<ISeeker>
    {
        public ISeeker ME { get; set; }
        public SeekBehaviour(ISeeker me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D(0, 0);
            return Seek(ME.Target.Pos, ME);

        }
    }
}
