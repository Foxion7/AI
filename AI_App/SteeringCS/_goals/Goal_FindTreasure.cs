using SteeringCS.entity;
using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_FindTreasure : GoalComponent
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_FindTreasure(string name, List<Goal> subgoals, Hero hero) : base(name, subgoals)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_FindTreasure(string name, List<Goal> subgoals) : base(name, subgoals)
        {
            started = false;
            done = false;
        }

        public override void Enter()
        {
            started = true;
            currentGoal = 0;
        }

        public override void Process()
        {
            if (subgoals.Count() > 0)
            {
                if (subgoals.Last().done)
                {
                    subgoals.Clear();
                    Exit();
                }
                else
                {
                    if (!subgoals[currentGoal].done && !subgoals[currentGoal].started)
                    {
                        subgoals[currentGoal].Enter();
                    }
                    else if (!subgoals[currentGoal].done && subgoals[currentGoal].started)
                    {
                        subgoals[currentGoal].Process();
                    }
                    else if (subgoals[currentGoal].done && currentGoal < subgoals.Count())
                    {
                        currentGoal++;
                    }
                }
            }
        }

        public override void Exit()
        {
            hero.RemoveDebugText(1);
            started = false;
            done = true;
        }
    }
}
