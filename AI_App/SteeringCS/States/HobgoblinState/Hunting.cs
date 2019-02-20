using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.HobgoblinState
{
    public class Hunting : IHobgoblinState
    {
        Hobgoblin hobgoblin;

        public Hunting(Hobgoblin hobgoblin)
        {
            this.hobgoblin = hobgoblin;
        }

        public void Act(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D(0, 0);

            if (hobgoblin._SB != null)
                steeringForce += hobgoblin._SB.Calculate() * 4;
            if (hobgoblin._OA != null)
                steeringForce += hobgoblin._OA.Calculate();
            if (hobgoblin._WA != null)
                steeringForce += hobgoblin._WA.Calculate();
            steeringForce.Truncate(hobgoblin.MaxForce);

            Vector2D acceleration = steeringForce / hobgoblin.Mass;

            hobgoblin.Velocity += (acceleration * timeElapsed);
            hobgoblin.Velocity = hobgoblin.Velocity.Truncate(hobgoblin.MaxSpeed);
            hobgoblin.OldPos = hobgoblin.Pos;
            hobgoblin.Pos += (hobgoblin.Velocity * timeElapsed);
            if (hobgoblin.Velocity.LengthSquared() > 0.00000001)
            {
                hobgoblin.Heading = hobgoblin.Velocity.Normalize();
                hobgoblin.Side = hobgoblin.Heading.Perp();
            }
            hobgoblin.WrapAround();
            hobgoblin.world.rePosGoblin(hobgoblin.Key, hobgoblin.OldPos, hobgoblin.Pos);
        }

        public override string ToString()
        {
            return "Hunting";
        }
    }
}
