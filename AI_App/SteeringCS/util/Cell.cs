using System.Collections.Generic;

namespace SteeringCS
{
    public class Cell<T>
    {
        //all the entities inhabiting this cell
        public List<T> Members;

        //the cell's bounding box
        public Vector2D TopLeft;
        public Vector2D BottomRight;

        public Cell(Vector2D topleft, Vector2D botright)
        {
            TopLeft = topleft;
            BottomRight = botright;
        }
    };
}
