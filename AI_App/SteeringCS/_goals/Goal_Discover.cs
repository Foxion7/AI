using SteeringCS.entity;
using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_Discover : Goal
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_Discover(string name, Hero hero) : base(name)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_Discover(string name) : base(name)
        {
            started = false;
            done = false;
        }

        int counter = 0;

        public override void Enter()
        {
            started = true;
        }

        public override void Process()
        {
            hero.AddDebugText("                                    " + name, 2);

            counter++;
            if (counter == 100)
            {
                Exit();
            }
        }

        public override void Exit()
        {
            done = true;
            started = false;

        }
    }
}
