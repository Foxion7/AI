using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    public abstract class SteeringBehaviour<TME> where TME : MovingEntity
    {
        public TME ME { get; set; }
        public abstract Vector2D Calculate();

        protected SteeringBehaviour(TME me)
        {
            ME = me;
        }
    }

    
}
