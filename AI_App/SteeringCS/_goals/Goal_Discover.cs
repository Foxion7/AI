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

        int counter = 0;
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
            hero.AddDebugText("                                    " + "target pos: " + pos, 3);
            Console.WriteLine(VectorMath.DistanceBetweenPositions(pos, hero.Pos) < 100);
            if (VectorMath.DistanceBetweenPositions(pos, hero.Pos) < 10) {
                pos = hero.getRandomTarget();
                hero.world.setPlayerRoute(pos);
            }
            else {
                hero.ApplyForce(hero.WA.Calculate(), hero.timeElapsed);
                hero.ApplyForce(hero.OA.Calculate(), hero.timeElapsed);
                hero.ApplyForce(hero.PB.Calculate(), hero.timeElapsed);
            }
            //counter++;
            //if (counter == 50)
            //{
            //    Exit();
            //}
        }

        public override void Exit()
        {
            hero.RemoveDebugText(2);

            done = true;
            started = false;
        }
    }
}
