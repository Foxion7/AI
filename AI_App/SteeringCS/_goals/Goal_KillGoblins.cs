using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_KillGoblins : GoalGroup
    {
        public Goal_KillGoblins(string compositeGoalName) : base(compositeGoalName, null)
        {
        }

        public Goal_KillGoblins(string compositeGoalName, List<GoalComponent> goals) : base(compositeGoalName, goals)
        {
        }

        GoalComponent currentGoal;

        public void Enter()
        {
            currentGoal = goals[0];
           
            Console.WriteLine("enter comp " + name);
        }

        public void Process()
        {
            executeGoal(currentGoal);
            exitCheck();

            Console.WriteLine("process comp " + name);
        }

        public void Exit()
        {
            Console.WriteLine("exit comp " + name);
        }

        private void executeGoal(GoalComponent goal)
        {
            goal.Enter();
            while (!goal.done)
            {
                goal.Process();
            }
        }

        private void exitCheck()
        {
            // Exits if all inner goals are concluded.
            foreach (Goal goal in goals)
            {
                if (goal.done)
                {
                    Exit();
                }
            }
        }
    }
}
