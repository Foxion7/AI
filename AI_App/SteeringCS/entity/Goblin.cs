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
    public class Goblin : MovingEntity, IFlocker, IFollower, IArriver, IObstacleAvoider, IWallAvoider
    {
        public Color VColor { get; set; }
        public double BraveryLimit { get; set; }

        //grouping behaviour
        public IEnumerable<IMover> Neighbors => MyWorld.GetGoblinNeighbors(this, NeighborsRange);
        public double NeighborsRange { get; set; }
        public double GroupValue { get; set; }

        //following behaviour
        public MovingEntity Leader { get; set; }
        public int FollowValue { get; set; }
        public int AvoidValue { get; set; }

        //flocking behaviour
        public int SeparationValue { get; set; }
        public int CohesionValue { get; set; }
        public int AlignmentValue { get; set; }

        //arriving behaviour
        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }

        //obstacle avoidance behaviour
        public List<IObstacle> Obstacles => MyWorld.getObstacles();

        //wall avoidance behaviour
        public List<IWall> Walls => MyWorld.getWalls();


        //the SteeringBehaviours
        public ISteeringBehaviour<Goblin> SB; //the aggressive anti non goblin behaviour
        public ISteeringBehaviour<Goblin> FB; //the grouping behaviour
        public ISteeringBehaviour<Goblin> OA; //the don't get hit by obstacles behaviour
        public ISteeringBehaviour<Goblin> WA; //the don't get hit by walls behaviour

        public Goblin(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 50;
            MaxSpeed = 5;
            MaxForce = 50;

            GroupValue = 10;
            NeighborsRange = 100;

            SeparationValue = 1;
            CohesionValue = 1;
            AlignmentValue = 1;

            FollowValue = 20;
            AvoidValue = 20;

            SB = new ArrivalBehaviour(this);
            FB = new FlockBehaviour(this);
            OA = new ObstacleAvoidance(this);
            WA = new WallAvoidance(this);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;
            BraveryLimit = 100;
            Scale = 4;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            if (MyWorld.getHobgoblins().Any())
            {
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

            if(SB != null)
                steeringForce += SB.Calculate() * 10;
            if (FB != null)
                steeringForce += FB.Calculate();
            if (OA != null)
                steeringForce += OA.Calculate();
            if (WA != null)
                steeringForce += WA.Calculate();
            steeringForce.Truncate(MaxForce);

            Vector2D acceleration = steeringForce / Mass;

            Velocity += (acceleration * timeElapsed);
            Velocity = Velocity.Truncate(MaxSpeed);
            OldPos = Pos;
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




            if (MyWorld.TriangleModeActive)
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

            if (MyWorld.VelocityVisible)
            {
                // Wall avoidance lines.
                double MAX_SEE_AHEAD = 15;
                Vector2D center = Pos + Velocity.Normalize() * MAX_SEE_AHEAD;
                Vector2D leftSensor = new Vector2D(Pos.X + ((Side.X - Heading.X) * -MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y) * -MAX_SEE_AHEAD /2));
                Vector2D rightSensor = new Vector2D(Pos.X + ((Side.X - Heading.X * -1) * MAX_SEE_AHEAD/2), Pos.Y + ((Side.Y - Heading.Y * -1) * MAX_SEE_AHEAD / 2));

                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)center.X, (int)center.Y);
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)leftSensor.X, (int)leftSensor.Y);
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)rightSensor.X, (int)rightSensor.Y);

                // Velocity
                //g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }
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

    }
}
