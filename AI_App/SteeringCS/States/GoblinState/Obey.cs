using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.GoblinState
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
            if(goblin.Commander.CurrentCommand == 0)
            {
                goblin.FollowingOrder = true;
                goblin.Target = goblin.Commander;
            }
        }

        public void Enter( )
        {
        }

        public void Exit( )
        {
        }

        public override string ToString()
        {
            return "Obeying";
        }
    }
}
