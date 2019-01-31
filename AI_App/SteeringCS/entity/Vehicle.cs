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
    public class Vehicle : MovingEntity, IArriver, IPursuer, IEvader
    {
        public Color VColor { get; set; }

        public SteeringBehaviour<Vehicle> SB;
        private MovingEntity _target;

        public Vehicle(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 5;
            MaxSpeed = 15;
            MaxForce = 50;
            PanicDistance = 100;
            Deceleration = Deceleration.fast;
            DecelerationTweaker = 5;
            VelocityTweaker = 0.8;
            SB = new SeekBehaviour<Vehicle>(this);
            Velocity = new Vector2D(0, 0);

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            if (Target != null)
            {
                Vector2D steeringForce = SB.Calculate();
                Vector2D acceleration = steeringForce / Mass;

                Velocity += (acceleration * timeElapsed);
                Velocity = Velocity.Truncate(MaxSpeed);

                Pos += (Velocity * timeElapsed);

                if (Velocity.LengthSquared() > 0.00000001)
                {
                    Heading = Velocity.Normalize();
                    Side = Heading.Perp();
                }
            }
            else if (Velocity.LengthSquared() > 0.00000001)
            {
                Velocity *= 0;
            }

            //DetectCollision();
            
            // Allows re-entry on other side of form if entity leaves.
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

        //public void DetectCollision()
        //{
        //    ahead = Pos + Velocity.Normalize();
        //    foreach (Obstacle obstacle in MyWorld.obstacles)
        //    {
        //        if (lineIntersectsCircleAhead(obstacle))
        //        {
        //            //Console.WriteLine(name);
        //            //Console.WriteLine("Collision detected!");
        //        }
        //    }
        //}

        //private bool lineIntersectsCircleAhead(Obstacle obstacle)
        //{
        //    // Optionally add ahead2 check.
        //    return DistanceBetweenPositions(obstacle.Pos, ahead) <= obstacle.Radius;
        //}

        //private double DistanceBetweenPositions(Vector2D pointA, Vector2D pointB)
        //{
        //    return Math.Sqrt((pointA.X - pointB.X) * (pointA.X - pointB.X) + (pointA.Y - pointB.Y) * (pointA.Y - pointB.Y));
        //}

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;

            Pen p = new Pen(VColor, 2);
            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            //g.DrawLine(p, (int) Pos.X, (int) Pos.Y, (int) Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            if(ahead != null)
            {
                g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X), (int)Pos.Y + (int)(Velocity.Y));
            }
        }

        public BaseGameEntity Target      { get; set; }
        public MovingEntity Evader        { get; set; }
        public MovingEntity Pursuer       { get; set; }
        public Deceleration Deceleration   { get; set; }
        public double DecelerationTweaker { get; set; }
        public double VelocityTweaker { get; set; }
        public double PanicDistance       { get; set; }
        public double PanicDistanceSq() => PanicDistance * PanicDistance;

    }
}
