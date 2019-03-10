using SteeringCS.entity;
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
        Hero hero;

        public Think(string name, List<Goal> goals, Hero hero) : base(name, goals)
        {
            done = false;
            this.hero = hero;
        }

        public override void Enter()
        {
        }

        public override void Process()
        {
            hero.AddDebugText("I am thinking", 0);

            // Choose a strategy here.
            if (CheckForGoblins())
            {
                hero.AddDebugText("Goblin found", 1);
            } else
            {
                hero.RemoveDebugText(1);
            }
        }

        public override void Exit()
        {
            done = true;
            hero.RemoveDebugText(0);
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

        private bool CheckForGoblins()
        {
            foreach (Goblin goblin in hero.world.getGoblins())
            {
                if(VectorMath.DistanceBetweenPositions(hero.Pos, goblin.Pos) < 500 && VectorMath.LineOfSight(hero.world, hero.Pos, goblin.Pos))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
