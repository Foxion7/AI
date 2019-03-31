using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS.entity
{
    public class Obstacle : BaseGameEntity, IObstacle
    {
        public Color VColor { get; set; }
        public double Radius { get; set; }
        public Vector2D Center { get; set; }

        public Obstacle(string name, double radius, Vector2D pos, World w) : base(name, pos, w)
        {
            Name = name;
            Radius = radius;
            Center = new Vector2D(pos.X + radius, pos.Y + radius);
            VColor = Color.Black;
        }

        public bool CollidesWith(Vector2D point) => (point - Center).LengthSquared() < Radius * Radius;

        public bool CollidesWith(Vector2D st, Vector2D ed)
        {
            var d = ed - st;
            var f = st - Center;
            var r = Radius;
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

        public override void Update(float delta)
        {
            
        }

        public override void Render(Graphics g)
        {
            double size = Radius * 2;

            //Brush b = new SolidBrush(ColorTranslator.FromHtml("#77797a"));
            //g.FillEllipse(b, new Rectangle((int)Pos.X, (int)Pos.Y, (int)size, (int)size));

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)Pos.X, (int)Pos.Y, (int)size, (int)size));

            if (world.DebugMode)
            {
                Brush brush = new SolidBrush(Color.Black);
                //g.DrawString(Name, SystemFonts.DefaultFont, brush, (float)(Pos.X + size / 2), (float)(Pos.Y - size / 2), new StringFormat());
            }
        }
    }
}
