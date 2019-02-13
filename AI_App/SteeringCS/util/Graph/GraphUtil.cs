using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using SteeringCS.Interfaces;

namespace SteeringCS.util.Graph
{
    public static class GraphUtil
    {
        public static VectorGraph CreateGraphForMap(int width, int height, int tileSize, List<IObstacle> obstacles,
            List<IWall> walls)
        {
            bool ObstacleCollidesWithPoint(IObstacle obstacle, Vector2D point) =>
                (point - obstacle.Center).LengthSquared() < obstacle.Radius * obstacle.Radius;

            bool WallCollidesWithPoint(IWall wall, Vector2D point)
            {
                var left   = wall.Pos.X - (wall.Width /2);
                var right  = wall.Pos.X + (wall.Width /2);
                var top    = wall.Pos.Y - (wall.Height/2);
                var bottom = wall.Pos.Y + (wall.Height/2);
                return left < point.X && right > point.X && top < point.Y && bottom > point.Y;
            }

            var memory = new Dictionary<(int, int), Vector2D>();
            var graph = new VectorGraph();
            for (int x = 0 / 2; x < width / tileSize; x++)
            {
                for (int y = 0 / 2; y < height / tileSize; y++)
                {
                    var current = new Vector2D(x * tileSize + tileSize / 2, y * tileSize + tileSize / 2);

                    if (obstacles.Any(obstacle => ObstacleCollidesWithPoint(obstacle, current)))
                        continue;
                    if (walls.Any(wall => WallCollidesWithPoint(wall, current)))
                        continue;

                    memory.Add((x, y), current);
                    graph.AddNode(current);
                    if (memory.ContainsKey((x - 1, y)))
                        graph.createEdge(memory[(x - 1, y)], current, tileSize);
                    
                    if (memory.ContainsKey((x - 1, y - 1)))
                        graph.createEdge(memory[(x - 1, y - 1)], current, tileSize + tileSize);

                    if (memory.ContainsKey((x, y - 1)))
                        graph.createEdge(memory[(x, y - 1)], current, tileSize);

                    if (memory.ContainsKey((x-1, y + 1)))
                        graph.createEdge(memory[(x-1, y + 1)], current, tileSize);

                }
            }

            return graph;
        }

        public static IEnumerable<GraphNode<T>> Route<T>(GraphNode<T> node)
        {
            var cur = node;
            while (cur.From != null)
            {
                yield return cur;
                cur = cur.From;
            }
            yield return cur;
        }

        //heuristic
        public static float Manhatten(GraphNode<Vector2D> n, GraphNode<Vector2D> m)
        {
            var x = Math.Abs(n.Data.X - m.Data.X);
            var y = Math.Abs(n.Data.Y - m.Data.Y);
            return (float)(y + x);
        }
        public static float EuclideanSq(GraphNode<Vector2D> n, GraphNode<Vector2D> m)
            => (float)(n.Data - m.Data).LengthSquared();
        public static float noHeuristic(GraphNode<Vector2D> n, GraphNode<Vector2D> m) => 0;

        public static Route AStar(VectorGraph graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end, Func<GraphNode<Vector2D>, GraphNode<Vector2D>, float> heuristic)
        {
            //this library priorityQueue will do until we have our own implementation
            FastPriorityQueue<GraphNode<Vector2D>> queue = new FastPriorityQueue<GraphNode<Vector2D>>(maxNodes: graph.Nodes.Count());
            start.Priority = 0;
            queue.Enqueue(start, start.Priority);
            while (queue.Count != 0)
            {
                GraphNode<Vector2D> current = queue.Dequeue();
                queue.Remove(current);
                current.Seen = true;
                if (current == end)
                    return new Route(Route(current).Reverse().Select(node => node.Data).ToList());
                foreach (var currentEdge in current.Edges)
                {
                    //if its a non directed list the node might have edged that "end" at it. in that case the "start" of the edge is the nextNode
                    var nextNode = currentEdge.Start == current ? currentEdge.End : currentEdge.Start;
                    if (!nextNode.Seen)
                    {
                        nextNode.Priority = (current.Priority + currentEdge.Value) + heuristic(nextNode, end);
                        nextNode.From = current;
                        queue.Enqueue(nextNode, nextNode.Priority);
                    }

                }
            }
            return new Route(null);
        }
        public static Route Dijkstra(VectorGraph graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end)
            => AStar(graph, start, end, heuristic: noHeuristic);

    }
}
