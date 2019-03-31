using SteeringCS.entity;
using SteeringCS.Goals;
using SteeringCS.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_Hunt: Goal
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_Hunt(string name, Hero hero) : base(name)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_Hunt(string name) : base(name)
        {
            done = false;
            started = false;
        }

        Vector2D pos;

        public override void Enter()
        {
            started = true;
        }

        public override void Process()
        {
            hero.AddDebugText("                                    " + name, 2);

            Goblin closestGoblin = null;
            double closestDistance = int.MaxValue;

            if (hero.world.getGoblins().Count() > 0)
            {
                // Selects next target.
                foreach (Goblin goblin in hero.world.getGoblins())
                {
                    double distance = VectorMath.DistanceBetweenPositions(hero.Pos, goblin.Pos);
                    if (distance < closestDistance && VectorMath.LineOfSight(hero.world, hero.Pos, goblin.Pos))
                    {
                        closestGoblin = goblin;
                        closestDistance = distance;
                    }
                }
                
                // If goblin is out of attack range, approaches.
                if (VectorMath.DistanceBetweenPositions(closestGoblin.Pos, hero.Pos) > hero.attackRange)
                {
                    hero.Path = new Route(new List<Vector2D>() { closestGoblin.Pos });
                }

                // If target is within range, exits to be able to attack.
                else if (hero.Path.Last() &&  VectorMath.DistanceBetweenPositions(closestGoblin.Pos, hero.Pos) < hero.attackRange)
                {
                    Exit();
                }

                hero.ApplyForce(hero.WA.Calculate(), hero.timeElapsed);
                hero.ApplyForce(hero.OA.Calculate(), hero.timeElapsed);
                hero.ApplyForce(hero.PB.Calculate(), hero.timeElapsed);
            }
        }

        public override void Exit()
        {
            hero.RemoveDebugText(2);

            done = true;
            started = false;
        }
    }
}
