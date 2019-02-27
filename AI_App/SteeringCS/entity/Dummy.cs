using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public class Dummy : IEntity
    {
        public string Name  { get; set; }
        public Vector2D Pos { get; set; }

        public Dummy(string name, Vector2D pos)
        {
            Name = name;
            Pos = pos;
        }

        public void Render(Graphics g)
        {
        }
    }
}
