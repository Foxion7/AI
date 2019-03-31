using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS._goals;
using SteeringCS.behaviour;
using SteeringCS.Goals;
using SteeringCS.Interfaces;
using SteeringCS.util;

namespace SteeringCS.entity
{
    public class Hero : MovingEntity, IObstacleAvoider, IWallAvoider
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

        private List<string> debugText;
        
        public Goal currentGoal;

        public FollowPathBehaviour PB;
        public ISteeringBehaviour OA;
        public ISteeringBehaviour WA;
        public Vector2D manualTarget { get;  set; }
        public Vector2D Center { get; set; }

        public int maxHealth { get; set; }
        public int maxStamina { get; set; }
        public int maxCooldown { get; set; }

        public int health { get; set; }
        public int stamina { get; set; }
        public int cooldown { get; set; }

        public int staminaCost { get; }
        public int cooldownCost { get; }

        public double currentGold { get; set; }
        
        public Hero(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            debugText = new List<string>();
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

            health = 100;
            cooldown = 100;
            stamina = 100;

            maxHealth = 100;
            maxStamina = 100;
            maxCooldown = 100;

            staminaCost = 50;
            cooldownCost = 100;

            currentGold = 0;

            Scale = 5;
            VColor = Color.Black;
            currentGoal = new Think("Think", this);
            currentGoal.Enter();
        }

        public override void Update(float timeElapsed)
        {
            currentGoal.Process();
            #region old stuff
            //Center = new Vector2D(Pos.X + Scale, Pos.Y + Scale);

            //Vector2D steeringForce = new Vector2D();

            //if (PB != null)
            //{
            //    steeringForce += PB.Calculate() * 0.33;
            //}
            //if (OA != null)
            //{
            //    steeringForce += OA.Calculate() * 0.66;
            //}
            //if (WA != null)
            //{
            //    steeringForce += WA.Calculate() * 0.66;
            //}

            //Vector2D acceleration = steeringForce / Mass;

            //Velocity += (acceleration * timeElapsed);
            //Velocity = Velocity.Truncate(MaxSpeed);
            //Pos += (Velocity * timeElapsed);

            //if (Velocity.LengthSquared() > 0.00000001)
            //{
            //    Heading = Velocity.Normalize();
            //    Side = Heading.Perp();
            //}
            //WrapAround();
            #endregion
        }

        public override void Render(Graphics g)
        {
            double leftCorner = Pos.X - Scale;
            double rightCorner = Pos.Y - Scale;
            double size = Scale * 2;
            Pen p = new Pen(VColor, 2);
            Pen r = new Pen(Color.Red, 1);

            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

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

            // Threat circle.
            if (world.DebugMode)
            {
                g.DrawEllipse(r, new Rectangle((int)(leftCorner - size * 2), (int)(rightCorner - size * 2), (int)size * 5, (int)size * 5));
            }
        }

        public void SetPath(Vector2D end)
        {
            Path = world.getRoute(this.Pos, end);
        }

        public Vector2D getRandomTarget() => world.RandomPos();
            
        public void Attack()
        {
            Goblin closestThreat = null;
            double closestDistance = 100;

            foreach (Goblin goblin in world.getGoblins())
            {
                if (VectorMath.DistanceBetweenPositions(Pos, goblin.Pos) < Scale * 5 && VectorMath.DistanceBetweenPositions(Pos, goblin.Pos) < closestDistance)
                {
                    closestThreat = goblin;
                    closestDistance = VectorMath.DistanceBetweenPositions(Pos, goblin.Pos);
                }
            }
            // If a target is nearby...
            if (closestThreat != null)
            {
                // Checks if there is enough resources for an attack.
                if (cooldown >= cooldownCost && stamina - staminaCost >= 0 && cooldown - cooldownCost >= 0)
                {
                    stamina -= staminaCost;
                    cooldown -= cooldownCost;
                    world.SpawnCorpse(closestThreat.Scale * 4, closestThreat.Pos);
                    world.DestroyGoblin(closestThreat);
                }
            }
        }

        public void RecoverHealth(int amount)
        {
            if (health + amount <= maxHealth)
            {
                health += amount;
            }
        }

        public void RecoverStamina(int amount)
        {
            if (stamina + amount <= maxStamina)
            {
                stamina += amount;
            }
        }

        public void RecoverCooldown(int amount)
        {
            if (cooldown + amount <= maxCooldown)
            {
                cooldown += amount;
            }
        }
        
        public void CollectTreasure(Treasure treasure)
        {
            currentGold += treasure.value;
            world.DestroyTreasure(treasure);
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

        public BaseGameEntity Target      { get; set; }
        public MovingEntity Evader        { get; set; }
        public MovingEntity Pursuer       { get; set; }
        public double PanicDistance       { get; set; }
        public double PanicDistanceSq() => PanicDistance * PanicDistance;
        public double SlowingRadius { get; set; }
        public double WanderJitter { get; set; }
        public double WanderRadius { get; set; }
        public double WanderDistance { get; set; }
        public List<IObstacle> Obstacles => world.getObstacles();

        public List<IWall> Walls => world.getWalls();
    }
}
