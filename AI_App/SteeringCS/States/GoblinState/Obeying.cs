using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.GoblinState
{
    public class Obeying : IGoblinState
    {
        Goblin goblin;
        int commandNr;

        public Obeying(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            if (commandNr == 0)
            {
                // Approach commander. (Commander should be the target temporarily)
                goblin.hunting.Act(timeElapsed);
            }
            else if(goblin.Commander.CurrentCommand == 1)
            {
                // Guarding current location.
                goblin.guarding.Act(timeElapsed);
            }
            else if(goblin.Commander.CurrentCommand == 2)
            {
                // Guarding current location.
                goblin.hunting.Act(timeElapsed);
            }
        }

        public void Enter( )
        {
            if (goblin.Commander != null)
            {
                commandNr = goblin.Commander.CurrentCommand;

                if (commandNr == 0)
                {
                    goblin.Target = goblin.Commander;

                }
            }
            goblin.FollowingOrder = true;

        }

        public void Exit( )
        {
            goblin.FollowingOrder = false;
        }

        public override string ToString()
        {
            return "Obeying";
        }
    }
}
