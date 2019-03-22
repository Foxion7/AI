using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS.util
{
    public static class Collisions
    {
        //all other collisions have moved to the relevant class
        public static bool LineCollidesWithLine(Vector2D a, Vector2D b, Vector2D c, Vector2D d)
        {
            double uA = ((d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X)) / ((d.Y - c.Y) * (b.X - a.X) - (d.X - c.X) * (b.Y - a.Y));
            
            double uB = ((b.X - a.X) * (a.Y - c.Y) - (b.Y - a.Y) * (a.X - c.X)) / ((d.Y - c.Y) * (b.X - a.X) - (d.X - c.X) * (b.Y - a.Y));
            
            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }

       
        
    }
}
