using SteeringCS.entity;
using SteeringCS.Interfaces;

namespace SteeringCS.States.GoblinState
{
    public class Wandering : IGoblinState
    {
        Goblin goblin;

        public Wandering(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D(0, 0);

            if (goblin._WB != null)
                steeringForce += goblin._WB.Calculate() * 0.5;
            if (goblin._FlockB != null)
                steeringForce += goblin._FlockB.Calculate();
            if (goblin._OA != null)
                steeringForce += goblin._OA.Calculate();
            if (goblin._WA != null)
                steeringForce += goblin._WA.Calculate();
            steeringForce.Truncate(goblin.MaxForce);

            Vector2D acceleration = steeringForce / goblin.Mass;

            goblin.Velocity += (acceleration * timeElapsed);
            goblin.Velocity = goblin.Velocity.Truncate(goblin.MaxSpeed /3);
            goblin.OldPos = goblin.Pos;
            goblin.Pos += (goblin.Velocity * timeElapsed);
            if (!goblin.Velocity.LenghtIsZero())
            {
                goblin.Heading = goblin.Velocity.Normalize();
                goblin.Side = goblin.Heading.Perp();
            }
            goblin.WrapAround();
            goblin.world.rePosGoblin(goblin.Key, goblin.OldPos, goblin.Pos);
        }

        public override string ToString()
        {
            return "Retreating";
        }
    }
}
