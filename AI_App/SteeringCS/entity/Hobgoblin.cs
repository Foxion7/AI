using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;

namespace SteeringCS.entity
{
    public class Hobgoblin : MovingEntity, IPursuer, ISeeker, IArriver, IObstacleAvoider
    {
        public Color VColor { get; set; }
        public ISteeringBehaviour<Hobgoblin> PB;
        public ISteeringBehaviour<Hobgoblin> FB;
        public ISteeringBehaviour<Hobgoblin> OA;

        public Hobgoblin(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 100;
            MaxSpeed = 1;
            MaxForce = 40;


            PB = new PursuitAndArriveBehaviour(this);
            OA = new ObstacleAvoidance(this);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;

            Scale = 10;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D steeringForce = PB.Calculate() *2;
            steeringForce += OA.Calculate();

            steeringForce.Truncate(MaxForce);

            Vector2D acceleration = steeringForce / Mass;

            Velocity += (acceleration * timeElapsed);
            Velocity = Velocity.Truncate(MaxSpeed);

            Pos += (Velocity * timeElapsed);

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }
            WrapAround();
        }
        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
        }
        
        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }
        public MovingEntity Evader { get; set; }
        public List<IObstacle> Obstacles => MyWorld.getObstacles();
    }
}
