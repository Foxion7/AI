using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Interfaces
{
    public interface IHobgoblinState
    {
        void Enter();
        void Act(float timeElapsed);
        void Exit();
    }
}
