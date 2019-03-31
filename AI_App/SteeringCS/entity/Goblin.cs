using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SteeringCS.behaviour;
using SteeringCS.Fuzzylogic;
using SteeringCS.Fuzzylogic.FuzzyTerms;
using SteeringCS.Interfaces;
using SteeringCS.States.GoblinState;

namespace SteeringCS.entity
{
    public class Goblin : MovingEntity, IGrouper, IObstacleAvoider, IWallAvoider
    {
        // For thread safety and message passing
        private static int _lastKey = 0;
        public readonly int Key;

        //fuzzy logic
        private FuzzyModule FuzzyLogicModule;
        
        //the higher this is the better his condition is. We could tie it to actual HP or something
        public int CurrentCondition = 80;
        //his rage score. Maybe make it tick up whenever he sees the hero character.
        public int CurrentAnger = 80;
        private List<string> debugText;
        public Color VColor { get; set; }
        public double BraveryDistance { get; set; }
        public double PassiveDistance { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }
        public int DamagePerAttack { get; set; }
        public int AttackRange { get; set; }
        public int AttackSpeed { get; set; }
        private readonly double _constMaxSpeed;
        public Hobgoblin Commander { get; set; }
        public bool FollowingOrder { get; set; }

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
        public IGoblinState currentState;
        public IGoblinState previousState;
        public IGoblinState hunting;
        public IGoblinState retreating;
        public IGoblinState guarding;
        public IGoblinState wandering;
        public IGoblinState regroup;
        public IGoblinState obey;
        public IGoblinState equip;
        
        public Goblin(string name, Vector2D pos, World w, MovingEntity Target) : base(name, pos, w)
        {
            //fuzzy logic
            FuzzyLogicModule = new FuzzyModule();
            var angerFLV = FuzzyLogicModule.CreateFLV("anger");
            FzSet calm      = angerFLV.AddLeftShoulderSet("calm", 0, 20, 40);
            FzSet irritated = angerFLV.AddTriangularSet("irritated", 20, 40, 50);
            FzSet angry     = angerFLV.AddRightShoulderSet("angry", 40, 50, 100);

            var healthFLV = FuzzyLogicModule.CreateFLV("health");
            FzSet sick = healthFLV.AddLeftShoulderSet("sick", 0, 30, 40);
            FzSet sickish = healthFLV.AddTriangularSet("sickish", 30, 40, 50);
            FzSet healthy = healthFLV.AddRightShoulderSet("healthy", 40, 50, 100);


            var speedFLV = FuzzyLogicModule.CreateFLV("speed");
            FzSet slow = speedFLV.AddLeftShoulderSet("slow", 20, 40, 60);
            FzSet mediocre = speedFLV.AddTriangularSet("mediocre", 40, 60, 80);
            FzSet fast = speedFLV.AddRightShoulderSet("fast", 60, 99, 100);

            FuzzyLogicModule.addRule(new FzAnd(calm, healthy), mediocre);
            FuzzyLogicModule.addRule(new FzAnd(calm, sickish), slow);
            FuzzyLogicModule.addRule(new FzAnd(calm, sick), slow);
            FuzzyLogicModule.addRule(new FzAnd(irritated, healthy), fast);
            FuzzyLogicModule.addRule(new FzAnd(irritated, sickish), mediocre);
            FuzzyLogicModule.addRule(new FzAnd(irritated, sick), slow);
            FuzzyLogicModule.addRule(new FzAnd(angry, healthy), fast);
            FuzzyLogicModule.addRule(new FzAnd(angry, sickish), fast);
            FuzzyLogicModule.addRule(new FzAnd(angry, sick), mediocre);

            // State.
            hunting = new Hunting(this);
            retreating = new Retreating(this);
            guarding = new Guarding(this);
            wandering = new Wandering(this);
            regroup = new Regroup(this);
            obey = new Obeying(this);
            equip = new Equip(this);
            SetState(guarding); // Starting state.

            FollowingOrder = false;

            Key = _lastKey + 1;
            _lastKey++;
            debugText = new List<string>();
            this.Target = Target;
            Mass = 50;
            _constMaxSpeed = 5;
            MaxSpeed = 5;
            MaxForce = 25;
            DamagePerAttack = 10;
            AttackRange = 10;
            AttackSpeed = 15; // Lower is faster.

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
            PassiveDistance = 1000; // Distance at which goblin goes to guard.
            BraveryDistance = 100;
            WanderRadius = 10;
            WanderDistance = 1;
            Scale = 4;
            VColor = Color.Black;

            AddDebugText("Current state: " + currentState, 0);
            AddDebugText("Previous state: " + previousState, 1);
        }

        public override void Update(float timeElapsed)
        {
            FuzzyLogicModule.Fuzzify("anger", CurrentAnger);
            FuzzyLogicModule.Fuzzify("health", CurrentCondition);

            var speedMod = FuzzyLogicModule.DeFuzzify("speed");
            MaxSpeed = (float) (_constMaxSpeed * speedMod / 100);
            currentState.Act(timeElapsed);
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
                double MAX_SEE_AHEAD = 15;
                Vector2D center = Pos + Heading * MAX_SEE_AHEAD;
                Vector2D leftSensor = new Vector2D(Pos.X + ((Side.X - Heading.X) * -MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y) * -MAX_SEE_AHEAD / 2));
                Vector2D rightSensor = new Vector2D(Pos.X + ((Side.X - Heading.X * -1) * MAX_SEE_AHEAD / 2), Pos.Y + ((Side.Y - Heading.Y * -1) * MAX_SEE_AHEAD / 2));

                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)center.X, (int)center.Y);
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)leftSensor.X, (int)leftSensor.Y);
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)rightSensor.X, (int)rightSensor.Y);

                // Velocity
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }

            if (world.DebugMode)
            {
                Brush brush = new SolidBrush(Color.Black);
                //g.DrawString(DebugText, SystemFonts.DefaultFont, brush, (float)(Pos.X + size), (float)(Pos.Y - size / 2), new StringFormat());
                
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
                        #region Localspace try
                        //if (obstacle.Name.Equals("obstacle4"))
                        //{
                        //    var angle = VectorMath.AngleBetweenPositions(Pos, obstacle.Center);

                        //    Console.WriteLine("Angle: " + angle);

                        //    double relativeX = obstacle.Center.X - Pos.X;
                        //    double relativeY = obstacle.Center.Y - Pos.Y;
                        //    double rotatedX = Math.Cos(-angle) * relativeX - Math.Sin(-angle) * relativeY;
                        //    double rotatedY = Math.Cos(-angle) * relativeY + Math.Sin(-angle) * relativeX;

                        //    Console.WriteLine("Obstacle location in local space: " + new Vector2D(rotatedX, rotatedY));
                        //}
                        #endregion
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
                    AddDebugText("No line of sight.", 2);
                }
                else
                {
                    AddDebugText("Line of sight!", 2);
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

        public void AddDebugText(string text, int index)
        {
            if (debugText != null)
            {
                // If not first text, adds new line.
                string newLine = "";
                if (debugText.Count() > 0 && index > 0)
                {
                    newLine += "\n";
                }
                newLine += text;

                bool doubleEntry = false;


                // Checks for double entries.
                for (int i = 0; i < debugText.Count(); i++)
                {
                    if (debugText[i].Equals(text))
                    {
                        doubleEntry = true;
                    }
                }

                // If index is not taken, adds line on index. Else (if not double) just adds at the end.
                if (debugText.Count() > index)
                {
                    debugText[index] = newLine;
                }
                else if (!doubleEntry)
                {
                    debugText.Insert(debugText.Count(), newLine);
                }
                
                // Adds all debugText texts together to display.
                string newDebugText = "";
                for(int i = 0; i < debugText.Count(); i++)
                {
                    newDebugText += debugText[i];
                }
                DebugText = newDebugText;
            }
        }

        public void RemoveDebugText(int index)
        {
            if (debugText != null)
            {
                if (debugText.Count() > index)
                {
                    debugText[index] = "";
                }
            }
        }

        public void SetState(IGoblinState state)
        {
            if (previousState != null)
            {
                previousState = currentState;

                previousState.Exit();
            }
            else
            {
                previousState = state;
            }

            state.Enter();
            currentState = state;

            AddDebugText("Current state: " + currentState, 0);
            AddDebugText("Previous state: " + previousState, 1);
        }

        public void Obey(Hobgoblin hobgoblin)
        {
            // If not already under someone's command...
            if(Commander == null)
            {
                Commander = hobgoblin;
                hobgoblin.Order += new Hobgoblin.OrderHandler(ReceivedOrder);
            }
        }

        public void Release(Hobgoblin hobgoblin)
        {
            if(hobgoblin.CurrentCommand == 0)
            {
                Target = world.Hero;
            }

            hobgoblin.Order -= ReceivedOrder;
            Commander = null;
            SetState(guarding);

            AddDebugText("Not obeying any orders.", 3);

        }

        // Method called when 'CallOrder' event triggers by Commander
        private void ReceivedOrder(Hobgoblin hobgoblin, int currentOrder)
        {
            if(!FollowingOrder)
                SetState(obey);

            AddDebugText("Obeying orders from " + hobgoblin.Name, 3);
        }
    }
}
