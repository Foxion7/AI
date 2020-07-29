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
            double angle = Math.Atan(y / x) * 180.0 / Math.PI;
            if (angle < 0)
            {
                angle *= -1;
            }
            return angle;
        }

        public static double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        {
            return (pointA - pointB).Length();
        }

        public static double Dot(this Vector2D v, Vector2D w)
        {
            var vNorm = v.Normalize();
            var wNorm = w.Normalize();
            var cosAlpha = vNorm.X * wNorm.X + vNorm.Y * wNorm.Y;
            return cosAlpha;
        }

        #region Collision


        //implementation correct
        public static bool ObstacleCollidesWithPoint(IObstacle obstacle, Vector2D point)
        {
            return (point - obstacle.Center).LengthSquared() < obstacle.Radius * obstacle.Radius;
        }

        //implementation correct
        public static bool WallCollidesWithPoint(IWall wall, Vector2D point)
        {
            var left = wall.Pos.X;
            var right = wall.Pos.X + (wall.Width);
            var top = wall.Pos.Y;
            var bottom = wall.Pos.Y + (wall.Height);
            return left < point.X &&
                   right > point.X &&
                   top < point.Y &&
                   bottom > point.Y;
        }


        public static bool ObstacleCollidesWithLine(IObstacle obstacle, Vector2D st, Vector2D ed)
        {
            if (ObstacleCollidesWithPoint(obstacle, st) || ObstacleCollidesWithPoint(obstacle, ed))
                return true;

            double distX = st.X - ed.X;
            double distY = st.Y - ed.Y;
            double len = Math.Sqrt((distX * distX) + (distY * distY));
            double dot = ((obstacle.Center.X - st.X) * (ed.X - st.X) + (obstacle.Center.Y - st.Y) * (ed.Y - st.Y)) / Math.Pow(len, 2);
            double closestX = st.X + (dot * (ed.X - st.X));
            double closestY = st.Y + (dot * (ed.Y - st.Y));
            return LineCollidesWithPoint(new Vector2D(closestX, closestY), st, ed);
        }


        public static bool WallCollidesWithLine(IWall wall, Vector2D st, Vector2D ed)
        {
            if (WallCollidesWithPoint(wall, st) || WallCollidesWithPoint(wall, ed))
                return true;

            var topLeft     = new Vector2D(wall.Pos.X, wall.Pos.Y);
            var topRight    = new Vector2D(wall.Pos.X + wall.Width, wall.Pos.Y);
            var bottomLeft  = new Vector2D(wall.Pos.X,wall.Pos.Y+wall.Height);
            var bottomRight = new Vector2D(wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);

            var left = LineCollidesWithLine(topLeft, bottomLeft, st, ed);
            var right = LineCollidesWithLine(topRight, bottomRight, st, ed);
            var top = LineCollidesWithLine(topLeft, topRight, st, ed);
            var bottom = LineCollidesWithLine(bottomLeft, bottomRight, st, ed);
            return (left || right || top || bottom);
        }


        private static bool LineCollidesWithPoint(Vector2D point, Vector2D st, Vector2D ed)
        {
            var dxc = point.X - st.X;
            var dyc = point.Y - st.Y;

            var dxl = ed.X - st.X;
            var dyl = ed.Y - st.Y;

            var cross = dxc * dyl - dyc * dxl;

            if (Math.Abs(cross) <= 0.1)
                return false;

            if (Math.Abs(dxl) >= Math.Abs(dyl))
            {
                if (dxl > 0)
                    return (st.X <= point.X && point.X <= ed.X);
                else
                    return (ed.X <= point.X && point.X <= st.X);
            }
            else
            {
                if (dyl > 0)
                    return (st.Y <= point.Y && point.Y <= ed.Y);
                else
                    return (ed.Y <= point.Y && point.Y <= st.Y);
            }
        }

        public static bool LineCollidesWithLine(Vector2D st1, Vector2D ed1, Vector2D st2, Vector2D ed2)
        {
            double denominator = ((ed1.X - st1.X) * (ed2.Y - st2.Y)) - ((ed1.Y - st1.Y) * (ed2.X - st2.X));
            double numerator1 = ((st1.Y - st2.Y) * (ed2.X - st2.X)) - ((st1.X - st2.X) * (ed2.Y - st2.Y));
            double numerator2 = ((st1.Y - st2.Y) * (ed1.X - st1.X)) - ((st1.X - st2.X) * (ed1.Y - st1.Y));

            // Detect coincident lines (has a problem, read below)
            if (Math.Abs(denominator) < 0.01) return Math.Abs(numerator1) < 0.01 && Math.Abs(numerator2) < 0.01;

            double r = numerator1 / denominator;
            double s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        #endregion
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