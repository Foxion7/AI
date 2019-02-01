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
    public class Vehicle : MovingEntity, IArriver, IPursuer, IEvader, IWanderer, ISeeker, IFleer
    {
        public Color VColor { get; set; }

        public ISteeringBehaviour<Vehicle> SB;

        public Vehicle(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 10;
            MaxSpeed = 30;
            MaxForce = 50;
            PanicDistance = 100;
            SB = new SeekBehaviour(this);
            Velocity = new Vector2D(0, 0);
            SlowingRadius = 300;

            WanderRadius = 1;
            WanderDistance = 0;
            WanderJitter = 1;

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            if(SB != null)
            {
                Vector2D steeringForce = SB.Calculate();
                Vector2D acceleration = steeringForce / Mass;

                Velocity += (acceleration * timeElapsed);
                Velocity = Velocity.Truncate(MaxSpeed);
            }

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
    }
}
