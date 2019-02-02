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
    public class Goblin : MovingEntity, IFlocker, IFollower, IPursuer, ISeeker, IArriver, IObstacleAvoider
    {
        public Color VColor { get; set; }

        public ISteeringBehaviour<Goblin> SB;
        public ISteeringBehaviour<Goblin> FB;
        public ISteeringBehaviour<Goblin> OA;

        public Goblin(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 100;
            MaxSpeed = 10;
            MaxForce = 50;
            SeparationValue = 128;
            CohesionValue = 4;
            AlignmentValue = 1;

            // Uncomment dit later misschien.
            //if (MyWorld.getHobgoblins().Any())
            //{
            //    Leader = GetClosestHobgoblin();
            //}
            FollowValue = 20;
            AvoidValue = 20;

            SB = new ArrivalBehaviour(this);
            FB = new FlockBehaviour(this);
            //FB = new LeaderFollowingBehaviour(this);
            OA = new ObstacleAvoidance(this);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;
            BraveryLimit = 200;

            Scale = 4;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            if (MyWorld.getHobgoblins().Any())
            {
                //Leader = GetClosestHobgoblin();
                Hobgoblin closestHobgoblin = GetClosestHobgoblin();

                double distancePlayerAndHobgoblin = VectorMath.DistanceBetweenPositions(MyWorld.Target.Pos, closestHobgoblin.Pos);

                if (distancePlayerAndHobgoblin > VectorMath.DistanceBetweenPositions(MyWorld.Target.Pos, Pos) && distancePlayerAndHobgoblin >= BraveryLimit)
                {
                    // If leader is far from player, follows leader.
                    Target = closestHobgoblin;
                }
                else
                {
                    // If leader is near player, attacks.
                    Target = MyWorld.Target;

                }
            }

            Vector2D steeringForce = new Vector2D(0, 0);

            steeringForce = SB.Calculate() * 20;
            steeringForce += FB.Calculate();
            steeringForce += OA.Calculate() * 0.5;
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
            Pen r = new Pen(Color.Red, 2);
            
            if (MyWorld.TriangleMode)
            {
                // Draws triangle.
                // Left lat
                g.DrawLine(p, (int)(Pos.X + (Side.X - Heading.X) * (size / 2)), (int)(Pos.Y + (Side.Y - Heading.Y) * (size / 2)), (int)(Pos.X) + (int)(Heading.X * (size / 2)), (int)Pos.Y + (int)(Heading.Y * (size / 2)));

                // Right lat
                g.DrawLine(p, (int)(Pos.X + ((Side.X * -1) - Heading.X) * (size / 2)), (int)(Pos.Y + ((Side.Y * -1) - Heading.Y) * (size / 2)), (int)Pos.X + (int)(Heading.X * (size / 2)), (int)Pos.Y + (int)(Heading.Y * (size / 2)));

                // Bottom lat
                g.DrawLine(p, (int)(Pos.X + ((Side.X * -1) - Heading.X) * (size / 2)), (int)(Pos.Y + ((Side.Y * -1) - Heading.Y) * (size / 2)), (int)(Pos.X + (Side.X - Heading.X) * (size / 2)), (int)(Pos.Y + (Side.Y - Heading.Y) * (size / 2)));
            }
            else
            {
                // Draws circle.
                g.DrawEllipse(p, new Rectangle((int)leftCorner, (int)rightCorner, (int)size, (int)size));
            }
            // Velocity
            g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
        }

        public Hobgoblin GetClosestHobgoblin()
        {
            Hobgoblin closestHobgoblin = null;
            double closestDistance = int.MaxValue;

            foreach(Hobgoblin hobgoblin in MyWorld.getHobgoblins())
            {
                double distance = VectorMath.DistanceBetweenPositions(Pos, hobgoblin.Pos);
                if(distance < closestDistance)
                {
                    closestHobgoblin = hobgoblin;
                    closestDistance = distance;
                }
            }

            return closestHobgoblin;
        }

        public IEnumerable<IMover> Neighbors => MyWorld.GetGoblinNeighbors(this, NeighborsRange);
        public double NeighborsRange { get; set; }
        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }
        public double BraveryLimit { get; set; }
        public MovingEntity Evader { get; set; }
        public int SeparationValue { get; set; }
        public int CohesionValue { get; set; }
        public int AlignmentValue { get; set; }
        public List<IObstacle> Obstacles => MyWorld.getObstacles();

        public MovingEntity Leader { get; set; }
        double IFollower.SeparationValue { get; }
        public double FollowValue { get; set; }
        public double AvoidValue { get; set; }
    }
}
