﻿using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.HobgoblinState
{
    public class Guarding : IHobgoblinState
    {
        Hobgoblin hobgoblin;

        public Guarding(Hobgoblin hobgoblin)
        {
            this.hobgoblin = hobgoblin;
        }

        public void Act(float timeElapsed)
        {
            //if (VectorMath.LineOfSight(goblin.world, goblin.Pos, goblin.world.Hero.Pos))
            //{
            //    // Turn to look.
            //}
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
