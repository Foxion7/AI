﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.IEntity;

namespace SteeringCS.entity
{
    public class Vehicle : MovingEntity, IArriver, IPursuer, IEvader, IWanderer
    {
        public Color VColor { get; set; }

        public SteeringBehaviour<Vehicle> SB;

        public Vehicle(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 10;
            MaxSpeed = 30;
            MaxForce = 30;
            PanicDistance = 100;
            SB = new SeekBehaviour<Vehicle>(this);
            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;

            WanderRadius = 1;
            WanderDistance = 0;
            WanderJitter = 1;

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D steeringForce = SB.Calculate();
            var x = double.IsNaN(steeringForce.X) ? 0 : steeringForce.X;
            var y = double.IsNaN(steeringForce.Y) ? 0 : steeringForce.Y;
            steeringForce = new Vector2D(x, y);
            Vector2D acceleration = steeringForce / Mass;

            Velocity += (acceleration * timeElapsed);
            Velocity = Velocity.Truncate(MaxSpeed);

            Pos += (Velocity * timeElapsed);

            if (Velocity.LengthSquared() > 0.00000001)
            {
                Heading = Velocity.Normalize();
                Side = Heading.Perp();
            }

            //DetectCollision();

            WrapAround();
        }
        // Allows re-entry on other side of form if entity leaves.
        private void WrapAround()
        {
            if (this.Pos.X > MyWorld.Width)
            {
                this.Pos = new Vector2D(1, Pos.Y);
            }
            else if (this.Pos.X < 0)
            {
                this.Pos = new Vector2D(MyWorld.Width - 1, Pos.Y);
            }

            if (this.Pos.Y > MyWorld.Height)
            {
                this.Pos = new Vector2D(Pos.X, 1);
            }
            else if (this.Pos.Y < 0)
            {
                this.Pos = new Vector2D(Pos.X, MyWorld.Height - 1);
            }
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