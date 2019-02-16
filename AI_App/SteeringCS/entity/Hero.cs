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
        
        public Hero(string name, Vector2D pos, World w) : base(name, pos, w)
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

            health = 100;
            cooldown = 100;
            stamina = 100;

            maxHealth = 100;
            maxStamina = 100;
            maxCooldown = 100;

            staminaCost = 50;
            cooldownCost = 50;

            Scale = 5;
            VColor = Color.Black;
        }

        public void Attack()
        {
            Console.WriteLine("Attacking");
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
            if(closestThreat != null)
            {
                // Checks if there is enough resources for an attack.
                if (cooldown >= cooldownCost && stamina - staminaCost >= 0 && cooldown - cooldownCost >= 0)
                {
                    Console.WriteLine("Successful attack");
                    stamina -= staminaCost;
                    cooldown -= cooldownCost;
                    // Kill goblin here.
                }
            }
        }

        public void RecoverHealth(int amount)
        {
            if(health + amount <= maxHealth)
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

        public override void Update(float timeElapsed)
        {
            Center = new Vector2D(Pos.X + Scale, Pos.Y + Scale);
            
            Vector2D steeringForce = new Vector2D();

            if (PB != null)
            {
                steeringForce += PB.Calculate() * 0.33;
            }
            if (OA != null)
            {
                steeringForce += OA.Calculate() * 0.66;
            }
            if (WA != null)
            {
                steeringForce += WA.Calculate() * 0.66;
            }

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

            g.DrawEllipse(p, new Rectangle((int) leftCorner, (int) rightCorner, (int) size, (int) size));
            g.DrawLine(p, (int)Pos.X, (int)Pos.Y, (int)Pos.X + (int)(Velocity.X * 2), (int)Pos.Y + (int)(Velocity.Y * 2));

            g.DrawEllipse(r, new Rectangle((int)( leftCorner - size * 2), (int)( rightCorner - size * 2), (int) size * 5, (int) size * 5));
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
