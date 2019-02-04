using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SteeringCS.util
{
    public class Cell<T>
    {
        //all the entities inhabiting this cell
        public ConcurrentDictionary<int, T> Members;

        //the cell's bounding box
        public InvertedBox BoundingBox;

        public Cell(Vector2D topleft, Vector2D botright)
        {
            Members = new ConcurrentDictionary<int, T>();
            BoundingBox = new InvertedBox(topleft, botright);
        }
    };
}
