using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.GoblinState
{
    public class Regroup : IGoblinState
    {
        Goblin goblin;

        public Regroup(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            // Find other goblins to group up with.
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
