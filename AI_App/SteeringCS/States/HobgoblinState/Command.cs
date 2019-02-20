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
            // Gives command to gathered goblins.

            // Command gather(goblins gather at hobgoblin)
            // Command attack(gathered goblins attack without hobgoblin)
            // Command protect((gathered goblins get between Hero and hobgoblin when hobgoblin is wounded)
            // Command guard(gathered goblins guard a spot)
        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
