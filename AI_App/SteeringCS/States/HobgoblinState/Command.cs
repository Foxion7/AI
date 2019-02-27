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
            // Gives order.
            hobgoblin.CallOrder();

            // Command gather(goblins gather at hobgoblin)          
            // Command release(goblins no longer obey hobgoblin)    

            // Command attack(gathered goblins attack without hobgoblin)
            // Command protect(gathered goblins get between Hero and hobgoblin when hobgoblin is wounded)
            // Command guard(gathered goblins guard a spot)

            StateCheck();
        }

        // Calls for goblins to stop obeying.
        public void Release()
        {
            foreach (Goblin goblin in hobgoblin.world.getGoblins())
            {
                goblin.Release(hobgoblin);
            }
            hobgoblin.AddDebugText("I don't have minions.", 2);
        }

        // Calls for goblins within range to start obeying.
        public void CallOut()
        {
            int debugCount = 0;
            foreach(Goblin goblin in hobgoblin.world.getGoblins())
            {
                if (VectorMath.DistanceBetweenPositions(hobgoblin.Pos, goblin.Pos) < hobgoblin.CommandRadius)
                {
                    debugCount++;
                    hobgoblin.AddDebugText("I have " + debugCount + " minions.", 2);

                    goblin.Obey(hobgoblin);
                }
            }
        }

        private void StateCheck()
        {
            if (VectorMath.DistanceBetweenPositions(hobgoblin.Pos, hobgoblin.Target.Pos) >= hobgoblin.CommandRadius || !VectorMath.LineOfSight(hobgoblin.world, hobgoblin.Pos, hobgoblin.Target.Pos))
            {
                hobgoblin.setState(hobgoblin.hunting);
            }
        }

        public void Enter( )
        {
            // Makes goblins listen for commands.
            CallOut();
        }

        public void Exit( )
        {

            // Releases goblins from commands.
            Release();
        }

        public override string ToString()
        {
            return "Commanding";
        }
    }
}
