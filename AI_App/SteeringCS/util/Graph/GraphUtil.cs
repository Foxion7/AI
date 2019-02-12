using System;
using System.Collections.Generic;
using System.Linq;
using Priority_Queue;
using SteeringCS.Interfaces;

namespace SteeringCS.util.Graph
{
    public static class GraphUtil
    {
        public static Graph<Vector2D> FloodFill(int width, int height, List<IObstacle> obstacles, List<IWall> walls)
        { 
            return new Graph<Vector2D>();

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


        public static Route Dijkstra(Graph<Vector2D> graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end)
            => AStar(graph, start, end, heuristic: (_1, _2) => 0);

        public static double Manhatten(GraphNode<Vector2D> n, GraphNode<Vector2D> m)
        {
            var x = Math.Abs(n.Data.X - m.Data.X);
            var y = Math.Abs(n.Data.Y - m.Data.Y);
            return y + x;
        }

        public static double EuclideanSq(GraphNode<Vector2D> n, GraphNode<Vector2D> m)
            => (n.Data - m.Data).LengthSquared();

        public static Route AStar(Graph<Vector2D> graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end, Func<GraphNode<Vector2D>, GraphNode<Vector2D>, float> heuristic)
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
    }
}
