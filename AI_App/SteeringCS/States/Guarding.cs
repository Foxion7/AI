﻿using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Guarding : IGoblinState
    {
        Goblin goblin;

        public Guarding(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}