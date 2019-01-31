using SteeringCS.IEntity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public class Obstacle : BaseGameEntity, IObstacle
    {
        public Color VColor { get; set; }
        public string name { get; set; }
        
        public Obstacle(string name, double radius, Vector2D pos, World w) : base(pos, w)
        {
            this.name = name;
            Radius = radius;
            VColor = Color.Black;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X + Radius;
            double rightCorner = Pos.Y + Radius;
            double size = Radius * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)Pos.X, (int)Pos.Y, (int)size, (int)size));
            g.DrawRectangle(p, (int)leftCorner, (int)rightCorner, 3, 3);
        }


        public double Radius {get; set;}
    }
}
