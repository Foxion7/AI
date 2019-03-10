using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_KillGoblins : GoalComponent
    {
        Goal currentGoal;
        
        public Goal_KillGoblins(string name, List<Goal> subgoals) : base(name, subgoals)
        {
        }

        public Goal_KillGoblins(string name) : base(name, null)
        {
        }

        public override void Enter()
        {
            currentGoal = subgoals[0];
           
            Console.WriteLine("enter comp " + name);
        }

        public override void Process()
        {
            ExecuteGoal(currentGoal);
            ExitCheck();

            Console.WriteLine("process comp " + name);
        }

        public override void Exit()
        {
            Console.WriteLine("exit comp " + name);
        }

        private void ExecuteGoal(Goal goal)
        {
            goal.Enter();
            while (!goal.done)
            {
                goal.Process();
            }
        }

        private void ExitCheck()
        {
            // Exits if all inner goals are concluded.
            foreach (Goal goal in subgoals)
            {
                if (goal.done)
                {
                    Exit();
                }
            }
        }
    }
}
