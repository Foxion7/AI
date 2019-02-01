using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.IEntity;

namespace SteeringCS.entity
{
    public class Goblin : MovingEntity, IFlocker, IPursuer, ISeeker, IArriver, IObstacleAvoidance
    {
        public Color VColor { get; set; }
        public SteeringBehaviour<Goblin> SB;
        public SteeringBehaviour<Goblin> FB;
        public SteeringBehaviour<Goblin> OA;

        public Goblin(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Mass = 100;
            MaxSpeed = 30;
            MaxForce = 50;
            SeparationValue = 128;
            CohesionValue = 4;
            AlignmentValue = 1;

            SB = new SeekBehaviour<Goblin>(this);
            FB = new FlockBehaviour<Goblin>(this);
            OA = new ObstacleAvoidance<Goblin>(this);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;

            Scale = 5;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            Vector2D steeringForce = SB.Calculate() *2;
            steeringForce += FB.Calculate();
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
                Console.WriteLine("Goblin " + this.name + "'s speed is: " + Velocity.Length());

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

        public IEnumerable<MovingEntity> Neighbors => MyWorld.getGoblinNeighbors(this, NeighborsRange);
        public double NeighborsRange { get; set; }
        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }
        public MovingEntity Evader { get; set; }
        public int SeparationValue { get; set; }
        public int CohesionValue { get; set; }
        public int AlignmentValue { get; set; }
        public double detectionboxLengthFactor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
