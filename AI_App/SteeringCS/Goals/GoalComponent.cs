using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public abstract class GoalComponent
    {
        public string name { get; set; }
        public bool done { get; set; }

        protected GoalComponent(string name)
        {
            this.name = name;
        }

        public void Enter()
        {
            //throw new NotImplementedException();
        }

        public void Process()
        {
            //throw new NotImplementedException();
        }

        public void Exit()
        {
            //throw new NotImplementedException();
        }
    }
}
