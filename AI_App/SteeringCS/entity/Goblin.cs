using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;
using SteeringCS.States;

namespace SteeringCS.entity
{
    public class Goblin : MovingEntity, IGrouper, IObstacleAvoider, IWallAvoider
    {
        // For thread safety.
        private static int _lastKey = 0;
        public readonly int Key;

        public Color VColor { get; set; }
        public double BraveryDistance { get; set; }
        public double PassiveDistance { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }

        // Grouping behaviour.
        public IEnumerable<IGrouper> Neighbors => world.GetGoblinNeighbors(this, NeighborsRange).Cast<IGrouper>();
        public FollowMode FollowMode { get; set; } = FollowMode.flock;

        // Avoidance behaviour.
        public List<IObstacle> Obstacles => world.getObstacles();
        public List<IWall> Walls => world.getWalls();

        // Steering behaviours.
        public ArrivalBehaviour _SB             { get; set; }
        public FleeBehaviour _FleeB             { get; set; }
        public WanderBehaviour _WB              { get; set; }
        public FlockBehaviour _FlockB           { get; set; }
        public LeaderFollowingBehaviour _LFB    { get; set; }
        public ObstacleAvoidance _OA            { get; set; }
        public WallAvoidance _WA                { get; set; }

        // States
        IGoblinState state;
        IGoblinState hunting;
        IGoblinState retreating;
        IGoblinState guarding;
        
        public Goblin(string name, Vector2D pos, World w, MovingEntity Target) : base(name, pos, w)
        {
            // State.
            hunting = new Hunting(this);
            retreating = new Retreating(this);
            guarding = new Guarding(this);
            setGoblinState(retreating); // Starting state.

            Key = _lastKey + 1;
            _lastKey++;
            this.Target = Target;
            Mass = 50;
            MaxSpeed = 5;
            MaxForce = 25;

            GroupValue = 10;
            NeighborsRange = 100;

            SeparationValue = 8;
            CohesionValue = 1;
            AlignmentValue = 16;

            FollowValue = 20;

            _SB = new ArrivalBehaviour(me: this, target: Target, slowingRadius: SlowingRadius);
            _FleeB = new FleeBehaviour(me: this, target: Target, panicDistance: PanicDistance);
            _FlockB = new FlockBehaviour(me: this, groupValue: GroupValue, cohesionValue: CohesionValue, alignmentValue: AlignmentValue, separationValue: SeparationValue);
            _LFB = new LeaderFollowingBehaviour(me: this, leader: Leader, slowingRadius: SlowingRadius, leaderBehindDist: 30, groupValue: GroupValue, followValue: FollowValue, separationValue: SeparationValue);
            _OA = new ObstacleAvoidance(this);
            _WA = new WallAvoidance(this);
            _WB = new WanderBehaviour(this, 100, 200);
            
            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;
            PanicDistance = 200; // Distance at which goblin starts fleeing.
            PassiveDistance = 250; // Distance at which goblin goes to guard.
            BraveryDistance = 100;
            WanderRadius = 10;
            WanderDistance = 1;
            Scale = 4;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            state.Act(timeElapsed);
            if (VectorMath.DistanceBetweenPositions(Pos, world.Hero.Pos) < PassiveDistance && VectorMath.LineOfSight(world, Pos, Target.Pos))
            {
                if (world.Hero.cooldown == 100)
                {
                    setGoblinState(retreating);
                }
                else
                {
                    setGoblinState(hunting);
                }
            }
            else
            {
                setGoblinState(guarding);
            }
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;
            Pen p = new Pen(VColor, 2);
            Pen r = new Pen(Color.Red, 2);
            
            if (world.TriangleModeActive)
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

            if (world.VelocityVisible)
            {
                // Wall avoidance lines.
                //double MAX_SEE_AHEAD = 15;
                //Vector2D center = Pos + Heading * MAX_SEE_AHEAD;
                //Vector2D leftSensor = new Vector2D(Pos.X + ((Side.X - Heading.X) * -MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y) * -MAX_SEE_AHEAD / 2));
                //Vector2D rightSensor = new Vector2D(Pos.X + ((Side.X - Heading.X * -1) * MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y * -1) * MAX_SEE_AHEAD / 2));

                //g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)center.X, (int)center.Y);
                //g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)leftSensor.X, (int)leftSensor.Y);
                //g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)rightSensor.X, (int)rightSensor.Y);

                // Velocity
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }

            if (world.DebugMode)
            {
                Brush brush = new SolidBrush(Color.Black);
                g.DrawString(DebugText, SystemFonts.DefaultFont, brush, (float)(Pos.X + size), (float)(Pos.Y - size / 2), new StringFormat());

                if (VectorMath.DistanceBetweenPositions(Pos, world.Hero.Pos) < PassiveDistance)
                {

                    Vector2D currentPosition = new Vector2D(Pos.X, Pos.Y);
                    Vector2D goalPosition = new Vector2D(world.Hero.Pos.X, world.Hero.Pos.Y);

                    double segmentDistance = 15;

                    var toTarget = goalPosition - currentPosition;
                    Vector2D step = (goalPosition - Pos).Normalize() * segmentDistance;

                    bool lineOfSightBlocked = false;

                    while (VectorMath.DistanceBetweenPositions(currentPosition, goalPosition) > segmentDistance)
                    {
                        currentPosition += step;
                        foreach (IObstacle obstacle in world.getObstacles())
                        {
                            if (VectorMath.DistanceBetweenPositions(currentPosition, obstacle.Center) <= obstacle.Radius)
                            {
                                lineOfSightBlocked = true;
                                break;
                            }
                        }
                        foreach (IWall wall in world.getWalls())
                        {
                            if (VectorMath.PointInWall(currentPosition, wall))
                            {
                                lineOfSightBlocked = true;
                                break;
                            }
                        }
                        if (!lineOfSightBlocked)
                        {
                            g.DrawEllipse(r, new Rectangle((int)currentPosition.X, (int)currentPosition.Y, 1, 1));
                        }
                        else
                        {
                            break;
                        }

                    }
                    if (lineOfSightBlocked)
                    {
                        DebugText = "No line of sight.";
                    }
                    else
                    {
                        DebugText = "Line of sight found!";
                    }
                }
            }
        }

        public Hobgoblin GetClosestHobgoblin()
        {
            Hobgoblin closestHobgoblin = null;
            double closestDistance = int.MaxValue;

            foreach (Hobgoblin hobgoblin in world.getHobgoblins())
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

        // Functions to tweak behaviour at runtime.
        #region behaviourTweaking
        private int _separationValue;
        private int _cohesionValue;
        private int _alignmentValue;
        private IMover _leader;
        private IEntity _target;
        private double _slowingRadius;
        private double _panicDistance;
        private int _followValue;

        public double NeighborsRange { get; set; }
        public double GroupValue { get; set; }

        // Following behaviour.
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
            if (_FlockB == null)
                _FlockB = new FlockBehaviour(me: this, groupValue: this.GroupValue, cohesionValue: this.CohesionValue,
                    alignmentValue: this.AlignmentValue, separationValue: this.SeparationValue);
        }

        // Flocking behaviour.
        public int SeparationValue
        {
            get => _separationValue;
            set
            {
                _separationValue = value;
                if (_FlockB != null) _FlockB.SeparationValue = value;
                if (_LFB != null) _LFB.SeparationValue = value;
            }
        }
        public int CohesionValue
        {
            get => _cohesionValue;
            set
            {
                _cohesionValue = value;
                if (_FlockB != null) _FlockB.CohesionValue = value;
            }
        }
        public int AlignmentValue
        {
            get => _alignmentValue;
            set
            {
                _alignmentValue = value;
                if (_FlockB != null) _FlockB.AlignmentValue = value;
            }
        }


        // Arriving behaviour.
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

        // Flee behaviour
        public double PanicDistance {
            get => _panicDistance;
            set {
                _panicDistance = value;
                if (_FleeB != null) _FleeB.PanicDistance = value;
            }
        }
        #endregion

        public void setGoblinState(IGoblinState state)
        {
            this.state = state;
            //DebugText = "Current state: " + state.ToString();
        }

        public IGoblinState getApproachState(){return hunting;}
        public IGoblinState getRetreatState(){return retreating;}
        public IGoblinState getGuardState(){return guarding;}
    }
}
