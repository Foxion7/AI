﻿using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Hunting : IGoblinState
    {
        Goblin goblin;

        public Hunting(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D(0, 0);

            if (goblin._SB != null)
                steeringForce += goblin._SB.Calculate() * 4;
            if (goblin._FlockB != null)
                steeringForce += goblin._FlockB.Calculate() * 0.5;
            if (goblin._OA != null)
                steeringForce += goblin._OA.Calculate();
            if (goblin._WA != null)
                steeringForce += goblin._WA.Calculate();
            steeringForce.Truncate(goblin.MaxForce);

            Vector2D acceleration = steeringForce / goblin.Mass;

            goblin.Velocity += (acceleration * timeElapsed);
            goblin.Velocity = goblin.Velocity.Truncate(goblin.MaxSpeed);
            goblin.OldPos = goblin.Pos;
            goblin.Pos += (goblin.Velocity * timeElapsed);
            if (goblin.Velocity.LengthSquared() > 0.00000001)
            {
                goblin.Heading = goblin.Velocity.Normalize();
                goblin.Side = goblin.Heading.Perp();
            }
            goblin.WrapAround();
            goblin.world.rePosGoblin(goblin.Key, goblin.OldPos, goblin.Pos);
        }

        public override string ToString()
        {
            return "Hunting";
        }
    }
}