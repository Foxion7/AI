using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;
using SteeringCS.Interfaces;

namespace SteeringCS.entity
{
    public abstract class MovingEntity : BaseGameEntity, IMover
    {
        public Vector2D Velocity { get; set; }
        public Vector2D Heading  { get; set; }
        public Vector2D Side     { get; set; }
        public float Mass        { get; set; }
        public float MaxSpeed    { get; set; }
        public float MaxForce    { get; set; }
        public float MaxTurnRate { get; set; }
        public Vector2D OldPos   { get; set; }
        private string debugText { get; set; }
        public string DebugText  {
            get { return debugText; }
            set {
                debugText = value.Replace("\n", Environment.NewLine);
            }
        }

        protected MovingEntity(string name, Vector2D pos, World w) : base(name, pos, w)
        {
            Pos = pos;
            Velocity = new Vector2D();
            Heading = new Vector2D(1,1);
            Side = Heading.Perp();
        }

        // Allows re-entry on other side of form if entity leaves.
        public void WrapAround()
        {
            if (this.Pos.X > world.Width)
            {
                this.Pos = new Vector2D(1, Pos.Y);
            }
            else if (this.Pos.X < 0)
            {
                this.Pos = new Vector2D(world.Width - 1, Pos.Y);
            }

            if (this.Pos.Y > world.Height)
            {
                this.Pos = new Vector2D(Pos.X, 1);
            }
            else if (this.Pos.Y < 0)
            {
                this.Pos = new Vector2D(Pos.X, world.Height - 1);
            }
        }

        public override string ToString()
        {
            return $"{Velocity}";
        }
    }
}
