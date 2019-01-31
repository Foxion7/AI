using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SteeringCS
{
   
    //Everything is pure in here.
    public struct Vector2D
    {
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector2D other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public double X { get; }
        public double Y { get; }

        public Vector2D(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
        }

        public static Vector2D operator + (Vector2D v, Vector2D w)
            => new Vector2D(w.X + v.X, w.Y + v.Y);
        
        public static Vector2D operator - (Vector2D v, Vector2D w)
            => new Vector2D(v.X - w.X, v.Y - w.Y);

        public static Vector2D operator * ( Vector2D v, double value)
            => new Vector2D(v.X * value, v.Y * value);
        
        public static Vector2D operator / (Vector2D v, double value)
            => new Vector2D(v.X / value, v.Y / value);

        public static bool operator ==(Vector2D v, Vector2D w)
            => Math.Abs(v.X - w.X) < 0.1 && Math.Abs(v.Y - w.Y) < 0.1;

        public static bool operator !=(Vector2D v, Vector2D w) 
            => !(v == w);

        public bool Equals(Vector2D other)
        {
            return this == other;
        }

        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }
}
