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
    public class Goblin : MovingEntity, IGrouper, IObstacleAvoider, IWallAvoider
    {
        ///for thread safety
        private static int _lastKey = 0;
        public readonly int Key;

        public Color VColor { get; set; }
        public double BraveryLimit { get; set; }

        //grouping behaviour
        public IEnumerable<IGrouper> Neighbors => MyWorld.GetGoblinNeighbors(this, NeighborsRange).Cast<IGrouper>();
        public FollowMode FollowMode { get; set; } = FollowMode.flock;

        //obstacle avoidance behaviour
        public List<IObstacle> Obstacles => MyWorld.getObstacles();

        //wall avoidance behaviour
        public List<IWall> Walls => MyWorld.getWalls();


        //the SteeringBehaviours
        private ArrivalBehaviour _SB;
        private FlockBehaviour _FB;
        private LeaderFollowingBehaviour _LFB;
        private ObstacleAvoidance _OA;
        private WallAvoidance _WA;


        public Goblin(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Key = _lastKey + 1;
            _lastKey++;

            Mass = 50;
            MaxSpeed = 5;
            MaxForce = 25;

            GroupValue = 10;
            NeighborsRange = 100;

            SeparationValue = 8;
            CohesionValue = 1;
            AlignmentValue = 16;

            FollowValue = 20;

            _SB = new ArrivalBehaviour(me: this, target: this.Target, slowingRadius: this.SlowingRadius);
            _FB = new FlockBehaviour(me: this, groupValue: this.GroupValue, cohesionValue: this.CohesionValue,
                alignmentValue: this.AlignmentValue, separationValue: this.SeparationValue);
            _LFB = new LeaderFollowingBehaviour(me: this, leader: this.Leader, slowingRadius: this.SlowingRadius,
                leaderBehindDist: 30, groupValue: this.GroupValue, followValue: this.FollowValue, separationValue: this.SeparationValue);
            _OA = new ObstacleAvoidance(this);
            _WA = new WallAvoidance(this);


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

            if (_SB != null)
                steeringForce += _SB.Calculate() * 4;
            if (_FB != null)
                steeringForce += _FB.Calculate();
            if (_OA != null)
                steeringForce += _OA.Calculate();
            if (_WA != null)
                steeringForce += _WA.Calculate();
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
            MyWorld.rePosGoblin(Key, OldPos, Pos);
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
                Vector2D center = Pos + Heading * MAX_SEE_AHEAD;
                Vector2D leftSensor = new Vector2D(Pos.X + ((Side.X - Heading.X) * -MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y) * -MAX_SEE_AHEAD / 2));
                Vector2D rightSensor = new Vector2D(Pos.X + ((Side.X - Heading.X * -1) * MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y * -1) * MAX_SEE_AHEAD / 2));

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

            foreach (Hobgoblin hobgoblin in MyWorld.getHobgoblins())
            {
                double distance = VectorMath.DistanceBetweenPositions(Pos, hobgoblin.Pos);
                if (distance < closestDistance)
                {
                    closestHobgoblin = hobgoblin;
                    closestDistance = distance;
                }
            }
            return closestHobgoblin;
        }

        //functions to tweak behaviour at runtime
        #region behaviourTweaking
        private int _separationValue;
        private int _cohesionValue;
        private int _alignmentValue;
        private IMover _leader;
        private IEntity _target;
        private double _slowingRadius;
        private int _followValue;

        public double NeighborsRange { get; set; }
        public double GroupValue { get; set; }

        //following behaviour
        public IMover Leader
        {
            get => _leader;
            set
            {
                _leader = value;
                if (_LFB != null) _LFB.Leader = value;
            }
        }
        public int FollowValue
        {
            get => _followValue;
            set
            {
                _followValue = value;
                if (_LFB != null) _LFB.FollowValue = value;
            }
        }
        public void Follow(IMover leader)
        {
            Leader = leader;
            FollowMode = FollowMode.groupFollow;
            if(_LFB == null)
                _LFB = new LeaderFollowingBehaviour(me: this, leader: this.Leader, slowingRadius: this.SlowingRadius, leaderBehindDist: 30, groupValue: this.GroupValue, followValue: this.FollowValue, separationValue: this.SeparationValue);
        }
        public void Flock()
        {
            FollowMode = FollowMode.flock;
            if (_FB == null)
                _FB = new FlockBehaviour(me: this, groupValue: this.GroupValue, cohesionValue: this.CohesionValue,
                    alignmentValue: this.AlignmentValue, separationValue: this.SeparationValue);
        }

        //flocking behaviour
        public int SeparationValue
        {
            get => _separationValue;
            set
            {
                _separationValue = value;
                if (_FB != null) _FB.SeparationValue = value;
                if (_LFB != null) _LFB.SeparationValue = value;
            }
        }
        public int CohesionValue
        {
            get => _cohesionValue;
            set
            {
                _cohesionValue = value;
                if (_FB != null) _FB.CohesionValue = value;
            }
        }
        public int AlignmentValue
        {
            get => _alignmentValue;
            set
            {
                _alignmentValue = value;
                if (_FB != null) _FB.AlignmentValue = value;
            }
        }


        //arriving behaviour
        public IEntity Target
        {
            get => _target;
            set
            {
                _target = value;
                if (_SB != null) _SB.Target = _target;
            }
        }
        public double SlowingRadius
        {
            get => _slowingRadius;
            set
            {
                _slowingRadius = value;
                if (_SB != null) _SB.SlowingRadius = value;
                if (_LFB != null) _LFB.SlowingRadius = value;
            }
        }
        #endregion

    }
}
