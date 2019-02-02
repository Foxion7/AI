using System.Collections.Generic;
using SteeringCS.util;

namespace SteeringCS
{
    public class Cell<T>
    {
        //all the entities inhabiting this cell
        public List<T> Members;

        //the cell's bounding box
        public Box boundingBox;

        public Cell(Vector2D topleft, Vector2D botright)
        {
            Members = new List<T>();
            boundingBox = new Box(topleft, botright);
        }
    };
}
