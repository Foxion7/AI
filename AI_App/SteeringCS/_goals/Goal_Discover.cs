using SteeringCS.entity;
using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_Discover : Goal
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_Discover(string name, Hero hero) : base(name)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_Discover(string name) : base(name)
        {
            started = false;
            done = false;
        }
        Vector2D pos;

        public override void Enter()
        {
            pos = hero.getRandomTarget();
            hero.world.setPlayerRoute(pos);
            started = true;
        }

        public override void Process()
        {
            hero.AddDebugText("                                    " + name, 2);

                // If treasure is sighted, stop looking.
                if (VectorMath.LineOfSight(hero.world, hero.Pos, hero.world.getTreasure()[0].Pos))
                {
                    Exit();
                }
                // If treasure is not sighted, create random goal position.
                else if (hero.Path.Last() && VectorMath.DistanceBetweenPositions(hero.Path.CurrentWaypoint(), hero.Pos) < 10)
                {
                    pos = hero.getRandomTarget();
                    hero.world.setPlayerRoute(pos);
                }
                // Moves to random position.
                else
                {
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
