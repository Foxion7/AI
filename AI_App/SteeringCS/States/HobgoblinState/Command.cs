using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SteeringCS.States.HobgoblinState
{
    public class Command : IHobgoblinState
    {
        Hobgoblin hobgoblin;

        public Command(Hobgoblin hobgoblin)
        {
            this.hobgoblin = hobgoblin;
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
