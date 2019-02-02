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
    public class Creature : MovingEntity, IArriver, IPursuer, IEvader, IWanderer, ISeeker, IFleer, IObstacleAvoider
    {
        public Color VColor { get; set; }

        public ISteeringBehaviour<Creature> SB;
        public ISteeringBehaviour<Creature> OA;

        public Creature(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 100;
            MaxSpeed = 10;
            MaxForce = 50;
            PanicDistance = 100;
            OA = new ObstacleAvoidance(this);
            Velocity = new Vector2D(0, 0);
            SlowingRadius = 300;

            WanderRadius = 20;
            WanderDistance = 0;
            WanderJitter = 40;

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D();
            if (SB != null)
            {
                steeringForce += SB.Calculate()*0.33;
            }
            if (OA != null)
            {
                steeringForce += OA.Calculate()*0.66;
            }

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
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
        }

        public BaseGameEntity Target      { get; set; }
        public MovingEntity Evader        { get; set; }
        public MovingEntity Pursuer       { get; set; }
        public double PanicDistance       { get; set; }
        public double PanicDistanceSq() => PanicDistance * PanicDistance;
        public double SlowingRadius { get; set; }
        public double WanderJitter { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }
        public List<IObstacle> Obstacles => MyWorld.getObstacles();
    }
}
