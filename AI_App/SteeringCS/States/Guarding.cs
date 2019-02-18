using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Guarding : IGoblinState
    {
        Goblin goblin;

        public Guarding(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Approach()
        {
            Console.WriteLine("Now approaching!");
            goblin.setGoblinState(goblin.getApproachState());

        }

        public void Attack()
        {
            Console.WriteLine("Now attacking!");
            goblin.setGoblinState(goblin.getAttackState());

        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void GroupUp()
        {
            throw new NotImplementedException();
        }

        public void Obey()
        {
            throw new NotImplementedException();
        }

        public void Retreat()
        {
            Console.WriteLine("Now retreating!");
            goblin.setGoblinState(goblin.getRetreatState());

        }

        public void Wander()
        {
            throw new NotImplementedException();
        }

        void IGoblinState.Guard()
        {
            Console.WriteLine("Already guarding.");

        }

        public override string ToString()
        {
            return "Guarding";
        }
    }
}
