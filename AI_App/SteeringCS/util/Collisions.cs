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
        //implementation correct
        public static bool ObstacleCollidesWithPoint(IObstacle obstacle, Vector2D point) =>
            (point - obstacle.Center).LengthSquared() < obstacle.Radius * obstacle.Radius;

        //implementation correct
        public static bool WallCollidesWithPoint(IWall wall, Vector2D point)
        {
            var left = wall.Pos.X;
            var right = wall.Pos.X + (wall.Width);
            var top = wall.Pos.Y;
            var bottom = wall.Pos.Y + (wall.Height);
            return left < point.X && right > point.X && top < point.Y && bottom > point.Y;
        }

        public static bool WallCollidesWithLine(IWall wall, Vector2D st, Vector2D ed)
        {
            var left = LineCollidesWithLine(st, ed, wall.Pos.X, wall.Pos.Y, wall.Pos.X, wall.Pos.Y + wall.Height);
            var right = LineCollidesWithLine(st, ed, wall.Pos.X + wall.Width, wall.Pos.Y, wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);
            var top = LineCollidesWithLine(st, ed, wall.Pos.X, wall.Pos.Y, wall.Pos.X + wall.Width, wall.Pos.Y);
            var bottom = LineCollidesWithLine(st, ed, wall.Pos.X, wall.Pos.Y + wall.Height, wall.Pos.X + wall.Width, wall.Pos.Y + wall.Height);
            return (left || right || top || bottom);
        }

        public static bool LineCollidesWithLine(Vector2D a, Vector2D b, double cx, double cy, double dx, double dy)
        {
            double uA = ((dx - cx) * (a.Y - cy) - (dy - cy) * (a.X - cx)) / ((dy - cy) * (b.X - a.X) - (dx - cx) * (b.Y - a.Y));
            
            double uB = ((b.X - a.X) * (a.Y - cy) - (b.Y - a.Y) * (a.X - cx)) / ((dy - cy) * (b.X - a.X) - (dx - cx) * (b.Y - a.Y));
            
            return uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1;
        }
        public static bool ObstacleCollidesWithLine(IObstacle obstacle, Vector2D st, Vector2D ed)
        {
            var d = ed - st;
            var f = st - obstacle.Center;
            var r = obstacle.Radius;
            double a = d.Dot(d);
            double b = 2 * f.Dot(d);
            double c = f.Dot(f) - r * r;

            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                // no intersection
                return false;
            }
            else
            {
                // ray didn't totally miss sphere,
                // so there is a solution to
                // the equation.

                discriminant = Math.Sqrt(discriminant);

                // either solution may be on or off the ray so need to test both
                // t1 is always the smaller value, because BOTH discriminant and
                // a are nonnegative.
                double t1 = (-b - discriminant) / (2 * a);
                double t2 = (-b + discriminant) / (2 * a);

                // 3x HIT cases:
                //          -o->             --|-->  |            |  --|->
                // Impale(t1 hit,t2 hit), Poke(t1 hit,t2>1), ExitWound(t1<0, t2 hit), 

                // 3x MISS cases:
                //       ->  o                     o ->              | -> |
                // FallShort (t1>1,t2>1), Past (t1<0,t2<0), CompletelyInside(t1<0, t2>1)

                if (t1 >= 0 && t1 <= 1)
                {
                    // t1 is the intersection, and it's closer than t2
                    // (since t1 uses -b - discriminant)
                    // Impale, Poke
                    return true;
                }

                // here t1 didn't intersect so we are either started
                // inside the sphere or completely past it
                if (t2 >= 0 && t2 <= 1)
                {
                    // ExitWound
                    return true;
                }

                // no intn: FallShort, Past, CompletelyInside
                return false;
            }
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
    }
}
