using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;

namespace SteeringCS.behaviour
{
    class EvasionBehaviour : SteeringBehaviour
    {
        public EvasionBehaviour(MovingEntity me) : base(me)
        {
        }

        public override Vector2D Calculate()
        {
            //    Vector2D ToPursuer = pursuer->Pos() - m_pVehicle->Pos();
            //    double LookAheadTime = ToPursuer.Length() / (m_pVehicle->MaxSpeed() + pursuer->Speed());
            //    return Flee(pursuer->Pos() + pursuer->Velocity() * LookAheadTime);
            return null;
        }
    }
}
