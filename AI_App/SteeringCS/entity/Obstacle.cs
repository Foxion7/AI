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
        
        public Obstacle(double radius, Vector2D pos, World w) : base(pos, w)
        {
            Radius = radius;
            VColor = Color.Black;
        }

        public override void Update(float delta)
        {
            throw new NotImplementedException();
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Radius * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
        }


        public double Radius {get; set;}
    }
}
