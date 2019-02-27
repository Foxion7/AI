using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    public class VectorGraph : Graph<Vector2D>
    {
        public virtual void Render(Graphics g)
        {
            foreach (var graphEdge in Edges)
            {
                Pen p = new Pen(graphEdge.Color, 2);
                g.DrawLine(p, (float)graphEdge.Start.Data.X, (float)graphEdge.Start.Data.Y, (float)graphEdge.End.Data.X, (float)graphEdge.End.Data.Y);
            }

            foreach (var graphNode in Nodes)
            {
                SolidBrush p = new SolidBrush(graphNode.Color);
                g.FillEllipse(p, new Rectangle((int)graphNode.Data.X-5, (int)graphNode.Data.Y-5, 10, 10));
                
            }
        }
    }
}
