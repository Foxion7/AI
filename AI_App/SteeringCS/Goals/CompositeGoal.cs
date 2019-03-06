using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public class CompositeGoal : GoalComponent
    {
        ArrayList goals { get; set; }
        string compositeGoalName { get; set; }

        public CompositeGoal(string compositeGoalName, ArrayList goals)
        {
            this.compositeGoalName = compositeGoalName;
            this.goals = goals;
        }

        public void addGoal(GoalComponent goal)
        {
            goals.Add(goal);
        }

        public void removeGoal(GoalComponent goal)
        {
            goals.Remove(goal);
        }

        public GoalComponent getGoal(int index)
        {
            return (GoalComponent)goals[index];
        }

        public void print()
        {
            foreach(GoalComponent goal in goals)
            {
                Console.WriteLine(goal.GetGoalName());
            }
        }
    }
}
