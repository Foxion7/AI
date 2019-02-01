using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class SeekBehaviour<TS> : SteeringBehaviour<TS> where TS: MovingEntity, ISeeker
    {
        public SeekBehaviour(TS me) : base(me)
        {

        }

        public override Vector2D Calculate()
        {
            if (ME.Target == null)
                return new Vector2D(0, 0);
            return Seek(ME.Target.Pos, ME);

        }
    }
}
