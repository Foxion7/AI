using System.Drawing;
using SteeringCS.Interfaces;

namespace SteeringCS
{
    public abstract class BaseGameEntity : IEntity
    {
        public Vector2D Pos { get; set; }
        public float Scale { get; set; }
        public World MyWorld { get; set; }
        public string Name { get; set; }

        protected BaseGameEntity(string name, Vector2D pos, World w)
        {
            Pos = pos;
            MyWorld = w;
            Name = name;
        }

        public abstract void Update(float delta);

        public virtual void Render(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, new Rectangle((int) Pos.X,(int) Pos.Y, 10, 10));
        }
    }
}
