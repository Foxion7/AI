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
                // Attack current target.
                goblin.hunting.Act(timeElapsed);
            }
            else if(goblin.Commander.CurrentCommand == 3)
            {
                // Stand in area between commander and hero to protect commander.
                IEntity targetZone = new Dummy("RendezvousZone", goblin.Commander.GetRendezvousPoint());
                goblin.Target = targetZone;
                
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
            if(goblin.Target != goblin.world.Hero)
            {
                goblin.Target = goblin.world.Hero;
            }
            goblin.FollowingOrder = false;
        }

        public override string ToString()
        {
            return "Obeying";
        }
    }
}
