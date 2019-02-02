using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.behaviour
{
    class WallAvoidance : ISteeringBehaviour<IWallAvoider>
    {
        public double MAX_SEE_AHEAD = 15;
        public double MAX_AVOID_FORCE = 100;
        public Vector2D ahead;
        public Vector2D ahead2;
        public IWallAvoider ME { get; set; }

        public WallAvoidance(IWallAvoider me)
        {
            ME = me;
        }

        public Vector2D Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
