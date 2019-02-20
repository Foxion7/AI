using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.States.HobgoblinState
{
    public class Retreating : IHobgoblinState
    {
        Hobgoblin hobgoblin;

        public Retreating(Hobgoblin hobgoblin)
        {
            this.hobgoblin = hobgoblin;
        }

        public void Act(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D(0, 0);

            if (hobgoblin._FleeB != null)
                steeringForce += hobgoblin._FleeB.Calculate() * 4;
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
            return "Retreating";
        }
    }
}
