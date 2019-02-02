using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util
{
    public class InvertedBox
    {
        public readonly Vector2D TopLeft;
        public readonly Vector2D BottomRight;

        public double Top => TopLeft.Y;
        public double Bottom => BottomRight.Y;
        public double Right => BottomRight.X;
        public double Left => TopLeft.X;


        public InvertedBox(Vector2D topLeft, Vector2D bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public bool Overlap(InvertedBox box)
        {
            return this.Left < box.Right && this.Right > box.Left &&
                   this.Top < box.Bottom && this.Bottom > box.Top;
        }
        
    }
}
