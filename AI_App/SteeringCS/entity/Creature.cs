using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;
using SteeringCS.util;

namespace SteeringCS.entity
{
    public class Creature : MovingEntity, IObstacleAvoider, IWallAvoider
    {
        public Color VColor { get; set; }
        private Route _path;
        public Route Path
        {
            get => _path;
            set
            {
                _path = value;
                PB.Path = value;
            }
        }

        public FollowPathBehaviour PB;
        public ISteeringBehaviour OA;
        public ISteeringBehaviour WA;
        public Vector2D manualTarget { get;  set; }

        public Creature(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 1;
            MaxSpeed = 10;
            MaxForce = 500;
            PanicDistance = 100;
            OA = new ObstacleAvoidance(this);
            WA = new WallAvoidance(this);
            PB = new FollowPathBehaviour(this, null, 1500, 100);
            Velocity = new Vector2D(0, 0);
            SlowingRadius = 300;

            WanderRadius = 50;
            WanderDistance = 0;
            WanderJitter = 40;

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D steeringForce = new Vector2D();

            //if (PB != null)
            //{
            //    steeringForce += PB.Calculate()*0.33;
            //}
            //if (OA != null)
            //{
            //    steeringForce += OA.Calculate() * 0.66;
            //}
            //if (WA != null)
            //{
            //    steeringForce += WA.Calculate() * 0.66;
            //}
            steeringForce = manualTarget;


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

            if (manualTarget != null)
            {
                g.DrawEllipse(p, new Rectangle((int)manualTarget.X, (int)manualTarget.Y, 5, 5));
            }

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

        public List<IWall> Walls => MyWorld.getWalls();
    }
}
