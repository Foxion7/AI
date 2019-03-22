using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SteeringCS.util.Collisions;
namespace SteeringCS.entity
{
    public class Wall : BaseGameEntity, IWall
    {
        public Color VColor { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Vector2D Center { get; set; }
        public bool CollidesWith(Vector2D point)
        {
            var left = Pos.X;
            var right = Pos.X + (Width);
            var top = Pos.Y;
            var bottom = Pos.Y + (Height);
            return left <= point.X && right >= point.X && top <= point.Y && bottom >= point.Y;

        }

        public bool CollidesWith(Vector2D st, Vector2D ed)
        {
            Vector2D width = new Vector2D(Width, 0);
            Vector2D height = new Vector2D(0, Height);
            Vector2D widthAndHeight = new Vector2D(Width, Height);
            var left = LineCollidesWithLine(st, ed, Pos, Pos + height);
            var right = LineCollidesWithLine(st, ed, Pos + width, Pos + widthAndHeight);
            var top = LineCollidesWithLine(st, ed, Pos, Pos + width);
            var bottom = LineCollidesWithLine(st, ed, Pos + height, Pos+ widthAndHeight);
            return (left || right || top || bottom);
        }

        public Wall(string name, double width, double height, Vector2D pos, World w) : base(name, pos, w)
        {
            this.Name = name;
            VColor = Color.Black;
            Width = width;
            Height = height;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics g)
        {

            //Brush b = new SolidBrush(ColorTranslator.FromHtml("#77797a"));
            //g.FillEllipse(b, new Rectangle((int)Pos.X, (int)Pos.Y, (int)size, (int)size));

            Pen p = new Pen(VColor, 2);
            g.DrawRectangle(p, (int)Pos.X, (int)Pos.Y, (int)Width, (int)Height);
        }

        private bool CollisionFound(Vector2D pos)
        {
            Vector2D topLeft = new Vector2D(Pos.X, Pos.Y);
            Vector2D bottomRight = new Vector2D(Pos.X + Width, Pos.Y + Height);

            return
                pos.X >= topLeft.X &&
                pos.X <= bottomRight.X &&
                pos.Y >= topLeft.Y &&
                pos.Y <= bottomRight.Y;
        }
    }
}
