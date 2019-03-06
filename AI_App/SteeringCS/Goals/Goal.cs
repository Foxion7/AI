using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public class Goal : GoalComponent
    {
        string name;

        public Goal(string name)
        {
            this.name = name;
        }

        public void Enter()
        {
            Console.WriteLine("enter " + name);
        }

        public void Process()
        {
            Console.WriteLine("process " + name);
        }

        public void Exit()
        {
            Console.WriteLine("exit " + name);
        }
    }
}
