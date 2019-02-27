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

        public override void Update(float delta)
        {
            throw new NotImplementedException();
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
                g.DrawString(Name, SystemFonts.DefaultFont, brush, (float)(Pos.X + size / 2), (float)(Pos.Y - size / 2), new StringFormat());
            }
        }

        public bool CollisionFound(Vector2D pos)
        {
            return VectorMath.DistanceBetweenPositions(pos, Center) <= Radius;
        }
    }
}
