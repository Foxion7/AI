using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SteeringCS
{
   
    //Everything is pure in here.
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Length()
            => Math.Sqrt(Math.Pow(X,2) + Math.Pow(Y,2));

        public double LengthSquared()
            => Math.Pow(X, 2) + Math.Pow(Y, 2);

        public Vector2D Add(Vector2D v)
            => new Vector2D(this.X + v.X, this.Y + v.Y);
        

        public Vector2D Sub(Vector2D v)
            => new Vector2D(this.X - v.X, this.Y - v.Y);

        public Vector2D Multiply(double value)
            => new Vector2D(this.X * value, this.Y * value);


        public Vector2D Divide(double value)
            => new Vector2D(this.X / value, this.Y /=value);
        
        public Vector2D Normalize()=> Divide(Length());

        public double DotProduct(Vector2D v)
            => (this.X * v.X) + (this.Y * v.Y); 

        public Vector2D Truncate(double max)
        {
            if (Length() > max)
            {
                var normalized = this.Normalize();
                return normalized.Multiply(max);
            }
            return this.Clone();
        }
        
        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }

        public Vector2D Perp()
        {
            return new Vector2D(-this.Y, this.X);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }


}
