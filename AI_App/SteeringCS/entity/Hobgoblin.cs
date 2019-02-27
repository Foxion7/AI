using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;
using SteeringCS.States.HobgoblinState;

namespace SteeringCS.entity
{
    public class Hobgoblin : MovingEntity, IObstacleAvoider, IWallAvoider
    {
        // For thread safety.
        private static int _lastKey = 0;
        public readonly int Key;

        private List<string> debugText;
        
        public Color VColor { get; set; }
        public double PassiveDistance { get; set; }
        public double PanicDistance { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }
        public int DamagePerAttack { get; set; }
        public int AttackRange { get; set; }
        public int AttackSpeed { get; set; }
        public int CurrentCommand { get; set; }
        public int CommandRadius { get; set; }

        // Avoidance behaviour.
        public List<IObstacle> Obstacles => world.getObstacles();
        public List<IWall> Walls => world.getWalls();

        // Steering behaviours.
        public ArrivalBehaviour _SB { get; set; }
        public FleeBehaviour _FleeB { get; set; }
        public WanderBehaviour _WB { get; set; }
        public ObstacleAvoidance _OA { get; set; }
        public WallAvoidance _WA { get; set; }

        // States
        public IHobgoblinState currentState;
        public IHobgoblinState previousState;
        public IHobgoblinState hunting;
        public IHobgoblinState retreating;
        public IHobgoblinState guarding;
        public IHobgoblinState wandering;
        public IHobgoblinState command;
        public IHobgoblinState equip;
        
        public event OrderHandler Order;
        public delegate void OrderHandler(Hobgoblin hobgoblin, int CurrentCommand);

        public Hobgoblin(string name, Vector2D pos, World w, MovingEntity Target) : base(name, pos, w)
        {
            // State.
            hunting = new Hunting(this);
            retreating = new Retreating(this);
            guarding = new Guarding(this);
            wandering = new Wandering(this);
            command = new Command(this);
            equip = new Equip(this);
            setState(guarding); // Starting state.
            Key = _lastKey + 1;
            _lastKey++;
            debugText = new List<string>();
            
            Mass = 100;
            MaxSpeed = 5;
            MaxForce = 40;

            DamagePerAttack = 25;
            AttackRange = 20;
            AttackSpeed = 30; // Lower is faster.
            CurrentCommand = 0; // Default command.
            CommandRadius = 125; // Size of area where goblins will respond to commanding.

            SlowingRadius = 100;
            PanicDistance = 200; // Distance at which hobgoblin starts fleeing.
            PassiveDistance = 250; // Distance at which hobgoblin goes to guard.

            _SB = new ArrivalBehaviour(me: this, target: Target, slowingRadius: SlowingRadius);
            _FleeB = new FleeBehaviour(me: this, target: Target, panicDistance: PanicDistance);
            _OA = new ObstacleAvoidance(this);
            _WA = new WallAvoidance(this);
            _WB = new WanderBehaviour(this, 100, 200);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;
            Scale = 10;
            VColor = Color.Black;

            AddDebugText("Current state: " + currentState, 0);
            AddDebugText("Previous state: " + previousState, 1);
        }

        public override void Update(float timeElapsed)
        {
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
                g.DrawLine(r, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));
            }

            if (world.DebugMode)
            {
                Brush brush = new SolidBrush(Color.Black);
                g.DrawString(DebugText, SystemFonts.DefaultFont, brush, (float)(Pos.X + size), (float)(Pos.Y - size / 2), new StringFormat());

                // Command circle.
                g.DrawEllipse(r, new Rectangle((int)leftCorner - CommandRadius + (int)(size /2), (int)rightCorner - CommandRadius + (int)(size / 2), CommandRadius * 2 , CommandRadius *2));
                
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
                        AddDebugText("No line of sight.", 2);
                    }
                    else
                    {
                        AddDebugText("Line of sight!", 2);
                    }
            }
        }

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
                for (int i = 0; i < debugText.Count(); i++)
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

        public void setState(IHobgoblinState state)
        {
            if(previousState != null)
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

        // Triggers all listeners and gives them the hobgoblin and command nr.
        public void CallOrder()
        {
            Order?.Invoke(this, CurrentCommand);
        }

        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }
        public MovingEntity Evader { get; set; }
    }
}
