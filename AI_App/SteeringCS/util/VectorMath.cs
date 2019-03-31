using System;
using SteeringCS.Interfaces;

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

        public static bool LenghtIsZero(this Vector2D v)
            => !(Math.Abs(v.X) > 0 || Math.Abs(v.Y) > 0);

        public static Vector2D Truncate(this Vector2D v, double max)
        {
            if (v.Length() > max)
            {
                var normalized = v.Normalize();
                return normalized * max;
            }
            return v.Clone();
        }
        
        public static double AngleBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            double x = pointB.X - pointA.X;
            double y = pointB.Y * -1 - pointA.Y * -1;
            double angle = Math.Atan(y / x) *180.0 / Math.PI;
            if (angle < 0)
            {
                angle *= -1;
            }
            return angle;
        }

        public static double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return (pointA-pointB).Length();
        }

        public static double Dot(this Vector2D v, Vector2D w)
        {
            var vNorm = v.Normalize();
            var wNorm = w.Normalize();
            var cosAlpha = vNorm.X * wNorm.X + vNorm.Y * wNorm.Y;
            return v.Length() * w.Length() * cosAlpha;
        }
        

        public static bool LineOfSight(World world, Vector2D posA, Vector2D posB)
        {
            Vector2D currentPosition = new Vector2D(posA.X, posA.Y);
            Vector2D goalPosition = new Vector2D(posB.X, posB.Y);

            double segmentDistance = 15;

            var toTarget = goalPosition - currentPosition;
            Vector2D step = (goalPosition - currentPosition).Normalize() * segmentDistance;

            while (DistanceBetweenPositions(currentPosition, goalPosition) > segmentDistance)
            {
                currentPosition += step;
                foreach (IObstacle obstacle in world.getObstacles())
                {
                    if (DistanceBetweenPositions(currentPosition, obstacle.Center) <= obstacle.Radius)
                    {
                        return false;
                    }
                }
                foreach (IWall wall in world.getWalls())
                {
                    if (PointInWall(currentPosition, wall))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool PointInWall(Vector2D point, IWall wall)
        {
            Vector2D topLeft = new Vector2D(wall.Pos.X, wall.Pos.Y);
            Vector2D bottomRight = new Vector2D(wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);

            return
                point.X >= topLeft.X &&
                point.X <= bottomRight.X &&
                point.Y >= topLeft.Y &&
                point.Y <= bottomRight.Y;
        }
    }
}