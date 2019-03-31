using SteeringCS.entity;
using SteeringCS.Goals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS._goals
{
    public class Goal_Collect : Goal
    {
        // TODO Remove. This is only used for debugTexts.
        Hero hero;

        // TODO Remove. This is only used for debugTexts.
        public Goal_Collect(string name, Hero hero) : base(name)
        {
            started = false;
            done = false;
            this.hero = hero;
        }

        public Goal_Collect(string name) : base(name)
        {
            started = false;
            done = false;
        }

        Vector2D pos;

        public override void Enter()
        {
            started = true;
        }

        public override void Process()
        {
            hero.AddDebugText("                                    " + name, 2);

            if(VectorMath.DistanceBetweenPositions(hero.Pos, hero.world.getTreasure()[0].Pos) > 10 && pos != hero.world.getTreasure()[0].Pos)
            {
                pos = hero.world.getTreasure()[0].Pos;
                hero.world.setPlayerRoute(pos);

            } else if (VectorMath.DistanceBetweenPositions(hero.Pos, hero.world.getTreasure()[0].Pos) <= 10)
            {
                hero.CollectTreasure(hero.world.getTreasure()[0]);
                Exit();
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

            started = false;
            done = true;
        }
    }
}
