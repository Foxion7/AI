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

            // Releases nearby listening goblins from commands.

            // Gives command to gathered goblins.

            // Command gather(goblins gather at hobgoblin)
            // Command release(goblins no longer obey hobgoblin)
            // Command attack(gathered goblins attack without hobgoblin)
            // Command protect((gathered goblins get between Hero and hobgoblin when hobgoblin is wounded)
            // Command guard(gathered goblins guard a spot)
            StateCheck();

        }

        // Calls for goblins to listen for commands.
        public void Release()
        {
            foreach (Goblin goblin in hobgoblin.world.getGoblins())
            {
                if (VectorMath.DistanceBetweenPositions(hobgoblin.Pos, goblin.Pos) < 500)
                {
                    goblin.Release(hobgoblin);
                }
            }
        }

        // Calls for goblins to listen for commands.
        public void CallOut()
        {
            foreach(Goblin goblin in hobgoblin.world.getGoblins())
            {
                if (VectorMath.DistanceBetweenPositions(hobgoblin.Pos, goblin.Pos) < 500)
                {
                    goblin.Obey(hobgoblin);
                }
            }
        }

        private void StateCheck()
        {
            if (VectorMath.DistanceBetweenPositions(hobgoblin.Pos, hobgoblin.Target.Pos) >= hobgoblin.PassiveDistance || !VectorMath.LineOfSight(hobgoblin.world, hobgoblin.Pos, hobgoblin.Target.Pos))
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
