using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public abstract class Goal
    {
        public string name { get; }
        public bool done { get; set; }
        public List<Goal> subgoals { get; set; }


        public Goal(string name)
        {
            this.name = name;
        }

        public Goal(string name, List<Goal> subgoals) : this(name)
        {
            this.subgoals = subgoals;
        }

        public override string ToString()
        {
            return name;
        }

        public abstract void Enter();

        public abstract void Process();

        public abstract void Exit();

        public void AddSubgoal()
        {
            throw new NotImplementedException();
        }


        public void Print()
        {
            Console.WriteLine(name);
            foreach (Goal goal in subgoals)
            {
                Console.WriteLine(goal);
            }
        }

    }
}
