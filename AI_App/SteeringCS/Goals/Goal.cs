using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public abstract class Goal
    {
        public string name { get; }
        public bool done { get; set; }
        
        public Goal(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }

        public abstract void Enter();

        public abstract void Process();

        public abstract void Exit();
    }
}
