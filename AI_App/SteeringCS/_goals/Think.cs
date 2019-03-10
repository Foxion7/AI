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
        public Think(string name, List<Goal> goals ) : base(name, goals)
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
            // Choose a strategy here.
        }

        public override void Exit()
        {
            done = true;

            Console.WriteLine("Done thinking.");
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
