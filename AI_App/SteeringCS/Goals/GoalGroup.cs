using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public class GoalGroup : GoalComponent
    {
        public List<GoalComponent> goals { get; set; }
        public string name { get; set; }

        public GoalGroup(string name, List<GoalComponent> goals):base(name)
        {
            this.name = name;
            this.goals = goals;
        }

        public GoalGroup(string compositeGoalName):this(compositeGoalName, null) {}
        
        public void Print()
        {
            foreach(GoalComponent goal in goals)
            {
                Console.WriteLine(goal);
            }
        }
    }
}
