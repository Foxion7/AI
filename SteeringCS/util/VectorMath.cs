﻿using System;

namespace SteeringCS
{
    public static class VectorMath
    {
        public static double Length(this Vector2D v)
            => Math.Sqrt(Math.Pow(v.X, 2) + Math.Pow(v.Y, 2));


        public static double LengthSquared(this Vector2D v)
            => Math.Pow(v.X, 2) + Math.Pow(v.Y, 2);


        public static Vector2D Normalize(this Vector2D v)
            => v / (v.Length());


        public static Vector2D Perp(this Vector2D v)
            => new Vector2D(v.Y, -v.X);


        public static Vector2D Truncate(this Vector2D v, double max)
        {
            if (v.Length() > max)
            {
                var normalized = v.Normalize();
                return normalized * max;
            }
            return v.Clone();
        }

        public static double Dot(this Vector2D v, Vector2D w)
        {
            var vNorm = v.Normalize();
            var wNorm = w.Normalize();
            var cosAlpha = vNorm.X * wNorm.X + vNorm.Y * wNorm.Y;
            return cosAlpha;
        }
    }
}