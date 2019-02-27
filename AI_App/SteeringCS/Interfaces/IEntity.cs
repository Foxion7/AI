using System.Drawing;

namespace SteeringCS.Interfaces
{
    public interface IEntity
    {
        string Name { get; }
        Vector2D Pos { get; }
        void Render(Graphics g);
    }
}