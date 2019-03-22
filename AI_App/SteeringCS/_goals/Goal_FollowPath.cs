using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.entity;
using SteeringCS.Goals;

namespace SteeringCS._goals
{
    class Goal_FollowPath : Goal
    {
        private Hero _hero;

        public Goal_FollowPath(string name, Hero hero) : base(name)
        {
            _hero = hero;
            started = false;
            done = false;
        }

        public override void Enter()
        {
            started = true;
        }

        public override void Process()
        {
            
        }

        public override void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
