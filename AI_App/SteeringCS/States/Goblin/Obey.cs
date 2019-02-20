using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Obey : IGoblinState
    {
        Goblin goblin;

        public Obey(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            // Only follows direct command by hobgoblin(s).
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
