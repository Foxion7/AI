using SteeringCS.entity;
using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_PlanPath: Goal
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_PlanPath(string name, Hero hero) : base(name)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_PlanPath(string name) : base(name)
        {
            done = false;
            started = false;
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
            if (counter == 50)
            {
                Exit();
            }
        }

        public override void Exit()
        {
            hero.RemoveDebugText(2);

            done = true;
            started = false;
        }
    }
}
