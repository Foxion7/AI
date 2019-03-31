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

            
            if (subgoals.Count() > 0 && subgoals.Last() != null)
            {
                hero.AddDebugText("Current Strategy: " + subgoals[currentGoal], 1);
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

        private Goal ChooseStrategy()
        {

            int threatScore = CalculateThreatScore();

            // Flight strategy. Not implemented yet.
            //if (threatScore > 10)
            //{
            //    Goal flee = new Goal_Flee("Flee", hero);
            //    return new Goal_Survive("Survive", new List<Goal> { flee }, hero);
            //}
            /* else */

            if (threatScore > 0)
            {
                Goal hunt = new Goal_Hunt("Hunt", hero);
                Goal attack = new Goal_Attack("Attack", hero);
                return new Goal_KillGoblins("Kill Goblins", new List<Goal> { hunt, attack }, hero);
            }
            else if (hero.world.getTreasure().Count() > 0)
            {
                Goal discover = new Goal_Discover("Discover", hero);
                Goal collect = new Goal_Collect("Collect", hero);
                return new Goal_FindTreasure("Find treasure", new List<Goal> { discover, collect }, hero);
            }
            return new Goal_Wander("Wander", hero);
        }

        private int CalculateThreatScore()
        {
            int threatScore = 0;
            int dangerDistance = 500;
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
