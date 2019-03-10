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
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Think(string name, Hero hero) : base(name, new List<Goal>())
        {
            done = false;
            this.hero = hero;
        }

        public Think(string name) : base(name, new List<Goal>())
        {
            done = false;
        }

        public override void Enter()
        {
            hero.AddDebugText("Start thinking", 0);
        }

        public override void Process()
        {
            hero.AddDebugText("I am thinking", 0);

            if (subgoals.Count > 0)
            {
                if (subgoals.Last().done)
                {
                    subgoals.Clear();
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
                        hero.AddDebugText("Current Strategy: " + subgoals[currentGoal], 1);
                    }
                    else if (subgoals[currentGoal].done && currentGoal < subgoals.Count())
                    {
                        currentGoal++;
                    }
                }
            }
            else
            {
                subgoals.Add(ChooseStrategy());
            }
        }

        public override void Exit()
        {
            hero.AddDebugText("Stopping thinking", 0);
            done = true;
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

        private GoalComponent ChooseStrategy()
        {
            int threatScore = CalculateThreatScore();
            if (threatScore > 10)
            {
                Goal flee = new Goal_Flee("Flee", hero);
                return new Goal_Survive("Survive", new List<Goal> { flee }, hero);
            }
            else if (threatScore > 0 && threatScore <= 10)
            {
                Goal attack = new Goal_Attack("Attack", hero);
                Goal hunt = new Goal_PlanPath("Hunt", hero);
                return new Goal_KillGoblins("Kill Goblins", new List<Goal> { attack, hunt }, hero);
            }
            else {
                Goal discover = new Goal_Discover("Discover", hero);
                Goal collect = new Goal_Collect("Collect", hero);
                return new Goal_FindTreasure("Find treasure", new List<Goal> { discover, collect }, hero); ;
            }
        }

        private int CalculateThreatScore()
        {
            int threatScore = 0;
            int dangerDistance = 250;

            foreach (Goblin goblin in hero.world.getGoblins())
            {
                if(VectorMath.DistanceBetweenPositions(hero.Pos, goblin.Pos) < dangerDistance && VectorMath.LineOfSight(hero.world, hero.Pos, goblin.Pos))
                {
                    threatScore += 5;
                }
            }
            return threatScore;
        }
    }
}
