using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_Hunt: Goal
    {
        public Goal_Hunt(string name) : base(name)
        {
            done = false;
        }

        int counter = 0;

        public override void Enter()
        {
            Console.WriteLine("enter " + name);
        }

        public override void Process()
        {
            counter++;

            if (counter == 100)
            {
                Exit();
            }
            Console.WriteLine("process attacking (counting) " + name);
        }

        public override void Exit()
        {
            Console.WriteLine("exit " + name);
            done = true;
        }
    }
}
