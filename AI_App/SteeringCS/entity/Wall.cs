using SteeringCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public class Wall : BaseGameEntity, IWall
    {
        public Color VColor { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public Vector2D Center { get; set; }

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
    }
}
