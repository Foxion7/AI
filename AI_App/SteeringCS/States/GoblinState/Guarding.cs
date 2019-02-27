using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States.GoblinState
{
    public class Guarding : IGoblinState
    {
        Goblin goblin;

        public Guarding(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Act(float timeElapsed)
        {
            StateCheck();
            //if (VectorMath.LineOfSight(goblin.world, goblin.Pos, goblin.world.Hero.Pos))
            //{
            //    // Turn to look.
            //}
        }

        private void StateCheck()
        {
            if (VectorMath.DistanceBetweenPositions(goblin.Pos, goblin.Target.Pos) < goblin.PassiveDistance && VectorMath.LineOfSight(goblin.world, goblin.Pos, goblin.Target.Pos))
            {
                goblin.setState(goblin.hunting);
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
            return "Guarding";
        }
    }
}
