﻿using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Equip : IGoblinState
    {
        Goblin goblin;

        public Equip(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            /// Tries to find and equip weapons / armor.
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
