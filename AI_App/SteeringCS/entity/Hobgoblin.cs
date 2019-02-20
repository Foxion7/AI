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
        public double BraveryDistance { get; set; }
        public double PassiveDistance { get; set; }
        public double PanicDistance { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }
        public int DamagePerAttack { get; set; }
        public int AttackRange { get; set; }
        public int AttackSpeed { get; set; }

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
        IHobgoblinState state;
        IHobgoblinState hunting;
        IHobgoblinState retreating;
        IHobgoblinState guarding;
        IHobgoblinState wandering;
        IHobgoblinState command;
        IHobgoblinState equip;

        public Hobgoblin(string name, Vector2D pos, World w, MovingEntity Target) : base(name, pos, w)
        {
            // State.
            hunting = new Hunting(this);
            retreating = new Retreating(this);
            guarding = new Guarding(this);
            wandering = new Wandering(this);
            command = new Command(this);
            equip = new Equip(this);
            setState(hunting); // Starting state.

            Key = _lastKey + 1;
            _lastKey++;
            debugText = new List<string>();
            
            Mass = 100;
            MaxSpeed = 5;
            MaxForce = 40;

            DamagePerAttack = 25;
            AttackRange = 20;
            AttackSpeed = 30; // Lower is faster.

            SlowingRadius = 100;
            PanicDistance = 200; // Distance at which goblin starts fleeing.
            PassiveDistance = 250; // Distance at which goblin goes to guard.
            BraveryDistance = 100;

            _SB = new ArrivalBehaviour(me: this, target: Target, slowingRadius: SlowingRadius);
            _FleeB = new FleeBehaviour(me: this, target: Target, panicDistance: PanicDistance);
            _OA = new ObstacleAvoidance(this);
            _WA = new WallAvoidance(this);
            _WB = new WanderBehaviour(this, 100, 200);

            Velocity = new Vector2D(0, 0);
            SlowingRadius = 100;

            Scale = 10;
            VColor = Color.Black;
        }

        public override void Update(float timeElapsed)
        {
            state.Act(timeElapsed);
            if (VectorMath.DistanceBetweenPositions(Pos, world.Hero.Pos) < PassiveDistance && VectorMath.LineOfSight(world, Pos, Target.Pos))
            {
                setState(hunting);
            }
            else
            {
                setState(guarding);
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
                        RemoveDebugText(1);
                    }
                    else
                    {
                        AddDebugText("Line of sight to Hero!", 1);
                    }
                }
                else
                {
                    RemoveDebugText(1);
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

                // If index is not taken, adds line on index. Else just adds at the end.
                if (debugText.Count() > index)
                {
                    debugText[index] = newLine;
                }
                else
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
            this.state = state;
            AddDebugText("Current state: " + state.ToString(), 0);
        }

        public BaseGameEntity Target { get; set; }
        public double SlowingRadius { get; set; }
        public MovingEntity Evader { get; set; }
    }
}
