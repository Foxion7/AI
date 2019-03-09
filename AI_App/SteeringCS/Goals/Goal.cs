using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Goals
{
    public class Goal : GoalComponent
    {
        public string name { get; }

        public Goal(string name):base(name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
