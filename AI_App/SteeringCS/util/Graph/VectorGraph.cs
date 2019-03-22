using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS.util.Graph
{
    public class VectorGraph : Graph<Vector2D>
    {

        public VectorGraph(int width, int height, int tileSize, List<IObstacle> obstacles,
            List<IWall> walls, double margin = 0)
        {
            double diagonal = Math.Sqrt(tileSize * tileSize * 2);

            var memory = new Dictionary<(int, int), Vector2D>();
            for (int x = 0; x < width / tileSize; x++)
            {
                for (int y = 0; y < height / tileSize; y++)
                {
                    var current = new Vector2D(x * tileSize + tileSize / 2, y * tileSize + tileSize / 2);

                    if (obstacles.Any(obstacle => obstacle.CollidesWith(current)))
                        continue;
                    if (walls.Any(wall => wall.CollidesWith(current)))
                        continue;

                    memory.Add((x, y), current);
                    AddNode(current);
                    if (memory.ContainsKey((x - 1, y)) &&
                        !walls.Any(wall => wall.CollidesWith(current, memory[(x - 1, y)])) &&
                        !obstacles.Any(obstacle => obstacle.CollidesWith(current, memory[(x - 1, y)])))
                    {
                        createEdge(memory[(x - 1, y)], current, tileSize);
                    }

                    if (memory.ContainsKey((x - 1, y - 1)) &&
                        !walls.Any(wall => wall.CollidesWith(current, memory[(x - 1, y - 1)])) &&
                        !obstacles.Any(obstacle => obstacle.CollidesWith(current, memory[(x - 1, y - 1)])))
                    {
                        createEdge(memory[(x - 1, y - 1)], current, (int)diagonal);
                    }

                    if (memory.ContainsKey((x, y - 1)) &&
                        !walls.Any(wall => wall.CollidesWith(current, memory[(x, y - 1)])) &&
                        !obstacles.Any(obstacle => obstacle.CollidesWith(current, memory[(x, y - 1)])))
                    {
                        createEdge(memory[(x, y - 1)], current, tileSize);
                    }

                    if (memory.ContainsKey((x - 1, y + 1)) &&
                        !walls.Any(wall => wall.CollidesWith(current, memory[(x - 1, y + 1)])) &&
                        !obstacles.Any(obstacle => obstacle.CollidesWith(current, memory[(x - 1, y + 1)])))
                    {
                        createEdge(memory[(x - 1, y + 1)], current, (int)diagonal);
                    }

                }
            }
        }
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
