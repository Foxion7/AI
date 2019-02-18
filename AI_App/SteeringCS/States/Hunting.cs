using SteeringCS.entity;
using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.States
{
    public class Hunting : IGoblinState
    {
        Goblin goblin;

        public Hunting(Goblin goblin)
        {
            this.goblin = goblin;
        }

        void IGoblinState.Approach()
        {
            Console.WriteLine("Already approaching.");
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

        public void Guard()
        {
            Console.WriteLine("Now guarding!");

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
    }
}
