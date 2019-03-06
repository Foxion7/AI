using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public abstract class GoalComponent
    {
        public void Enter()
        {
            throw new NotImplementedException();
        }

        public void Process()
        {
            throw new NotImplementedException();

        }

        public void Exit()
        {
            throw new NotImplementedException();
        }

        public void AddGoal(GoalComponent goal)
        {
            throw new NotImplementedException();
        }


        public void RemoveGoal(GoalComponent goal)
        {
            throw new NotImplementedException();
        }


        public string GetGoalName()
        {
            throw new NotImplementedException();
        }
    }
}
