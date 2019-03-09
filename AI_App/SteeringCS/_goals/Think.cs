using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Think : GoalComponent
    {
        public Think(string name) : base(name)
        {
            done = false;
        }

        public void Enter()
        {
            Console.WriteLine("Starting thinking...");
        }

        public void Process()
        {
            Console.WriteLine("Thinking...");

        }

        public void Exit()
        {
            done = true;

            Console.WriteLine("Done thinking.");
        }
    }
}
