using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Think : Goal
    {
        public Think(string name, List<Goal> goals ) : base(name)
        {
            done = false;
        }

        public override void Enter()
        {
            Console.WriteLine("Starting thinking...");
        }

        public override void Process()
        {
            Console.WriteLine("Thinking...");

        }

        public override void Exit()
        {
            done = true;

            Console.WriteLine("Done thinking.");
        }
    }
}
