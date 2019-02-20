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
        

        public static double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return (pointA-pointB).Length();
        }

        public static double Dot(this Vector2D v, Vector2D w)
        {
            var vNorm = v.Normalize();
            var wNorm = w.Normalize();
            var cosAlpha = vNorm.X * wNorm.X + vNorm.Y * wNorm.Y;
            return cosAlpha;
        }

        //implementation correct
        public static bool ObstacleCollidesWithPoint(IObstacle obstacle, Vector2D point) =>
    (point - obstacle.Center).LengthSquared() < obstacle.Radius * obstacle.Radius;

        //implementation correct
        public static bool WallCollidesWithPoint(IWall wall, Vector2D point)
        {
            var left = wall.Pos.X - (wall.Width);
            var right = wall.Pos.X + (wall.Width);
            var top = wall.Pos.Y - (wall.Height);
            var bottom = wall.Pos.Y + (wall.Height);
            return left < point.X && right > point.X && top < point.Y && bottom > point.Y;
        }

        public static bool ObstacleCollidesWithLine(IObstacle obstacle, Vector2D st, Vector2D ed)
        {
            //double distX = st.X - ed.X;
            //double distY = st.Y - ed.Y;
            //double len = Math.Sqrt((distX * distX) + (distY * distY));
            //double dot = (((obstacle.Center.X - st.X) * (ed.X - st.X)) + ((obstacle.Center.Y - st.Y) * (ed.Y - st.Y))) / Math.Pow(len, 2);
            //double closestX = st.X + (dot * (ed.X - st.X));
            //double closestY = st.Y + (dot * (ed.Y - st.Y));
            //return LineCollidesWithPoint(new Vector2D(closestX, closestY), st, ed);
            return false;
        }

        public static bool LineOfSight(World world, Vector2D posA, Vector2D posB)
        {
            Vector2D currentPosition = new Vector2D(posA.X, posA.Y);
            Vector2D goalPosition = new Vector2D(world.Hero.Pos.X, world.Hero.Pos.Y);

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

        private static bool LineCollidesWithPoint(Vector2D point, Vector2D st, Vector2D ed)
        {
            //double lineLen = (st - ed).Length();
            //double d1 = (point - st).Length();
            //double d2 = (point - ed).Length();
            //return Math.Abs(lineLen - (d1 + d2)) < 3;
            return false;
        }

        public static bool WallCollidesWithLine(IWall wall, Vector2D st, Vector2D ed)
        {
            //var left = LineCollidesWithLine(st, ed, wall.Center.X, wall.Center.Y, wall.Center.X, wall.Center.Y + wall.Height);
            //var right = LineCollidesWithLine(st, ed, wall.Center.X + wall.Width, wall.Center.Y, wall.Center.X + wall.Width, wall.Center.Y + wall.Height);
            //var top = LineCollidesWithLine(st, ed, wall.Center.X, wall.Center.Y, wall.Center.X + wall.Width, wall.Center.Y);
            //var bottom = LineCollidesWithLine(st, ed, wall.Center.X, wall.Center.Y + wall.Height, wall.Center.X + wall.Width, wall.Center.Y + wall.Height);
            //return (left || right || top || bottom);
            return false;
        }

        public static bool LineCollidesWithLine(Vector2D a, Vector2D b, double cx, double cy, double dx, double dy)
        {
            //double uA = ((dx - cx) * (a.Y - cy) - (dy - cy) * (a.X - cx)) / ((dy - cy) * (b.X - a.X) - (dx - cx) * (b.Y - a.Y));
            //
            //double uB = ((b.X - a.X) * (a.Y - cy) - (b.Y - a.Y) * (a.X - cx)) / ((dy - cy) * (b.X - a.X) - (dx - cx) * (b.Y - a.Y));
            //
            //return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
            return false;
        }
        public static bool LineCollidesWithLine(Vector2D a, Vector2D b, Vector2D c, Vector2D d)
        {
            //double uA = ((d.X - c.X) * (a.Y - c.Y) - (d.Y - c.Y) * (a.X - c.X)) / ((d.Y - c.Y) * (b.X - a.X) - (d.X - c.X) * (b.Y - a.Y));
            //
            //double uB = ((b.X - a.X) * (a.Y - c.Y) - (b.Y - a.Y) * (a.X - c.X)) / ((d.Y - cy) * (b.X - a.X) - (d.X - c.X) * (b.Y - a.Y));
            //
            //return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
            return false;
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