using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Attacking : IGoblinState
    {
        Goblin goblin;

        public Attacking(Goblin goblin)
        {
            this.goblin = goblin;
        }

        public void Approach()
        {
            Console.WriteLine("Now approaching!");
            goblin.setGoblinState(goblin.getApproachState());

        }

        public void Equip()
        {
            throw new NotImplementedException();
        }

        public void GroupUp()
        {
            throw new NotImplementedException();
        }

        public void Guard()
        {
            Console.WriteLine("Now guarding!");
            goblin.setGoblinState(goblin.getGuardState());

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

        public void Attack()
        {
            Console.WriteLine("Already attacking.");

        }

        public override string ToString()
        {
            return "Attacking";
        }
    }
}
