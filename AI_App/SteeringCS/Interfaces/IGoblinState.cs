using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Interfaces
{
    public interface IGoblinState
    {
        void Approach();
        void Attack();
        void Retreat();
        void Guard();
        void Wander();
        void GroupUp();
        void Obey(); // Do what leader says
        void Equip();
    }
}
