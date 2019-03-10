using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public abstract class GoalComponent : Goal
    {
        public List<Goal> subgoals { get; set; }
        public int currentGoal { get; set; }
        
        public GoalComponent(string name, List<Goal> subgoals) : base(name)
        {
            this.subgoals = subgoals;
        }

        public override string ToString()
        {
            return name;
        }

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
