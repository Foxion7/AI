using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Priority_Queue;
namespace SteeringCS.util.Graph
{
    public static class PathFinding
    {
        public static GraphNode<Vector2D> FindClosestNode(VectorGraph graph, Vector2D pos)
        {
            return graph.Nodes.Aggregate((node1, node2) =>
            {
                if ((node1.Data - pos).LengthSquared() < (node2.Data - pos).LengthSquared())
                    return node1;
                return node2;
            });
        }
        public static IEnumerable<GraphNode<T>> Route<T>(GraphNode<T> node)
        {
            var cur = node;
            while (cur.From != null)
            {
                cur.Traveled.Color = Color.Red;
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
        public static float Euclidean(GraphNode<Vector2D> n, GraphNode<Vector2D> m)
            => (float)(n.Data - m.Data).Length();
        public static float noHeuristic(GraphNode<Vector2D> n, GraphNode<Vector2D> m) => 0;

        public static IEnumerable<Vector2D> AStar(Graph<Vector2D> graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end, Func<GraphNode<Vector2D>, GraphNode<Vector2D>, float> heuristic)
        {
            foreach (var n in graph.Nodes)
            {
                n.Visited = false;
                n.Calculated = false;
                n.Priority = 0;
                n.From = null;
                n.Traveled = null;
                n.Color = Color.Black;
            }
            foreach (var e in graph.Edges)
            {
                e.Color = Color.Gray;
            }

            //this library priorityQueue will do until we have our own implementation
            FastPriorityQueue<GraphNode<Vector2D>> queue = new FastPriorityQueue<GraphNode<Vector2D>>(graph.Nodes.Count());
            start.Priority = 0;
            queue.Enqueue(start, start.Priority);
            while (queue.Count != 0)
            {
                GraphNode<Vector2D> current = queue.Dequeue();
                current.Visited = true;
                current.Color = Color.Blue;
                if (current == end)
                {
                    return Route(current).Reverse().Select(nd => nd.Data);
                }

                foreach (var currentEdge in current.Edges)
                {
                    //if its a non directed list the node might have edged that "end" at it. in that case the "start" of the edge is the nextNode
                    var nextNode = currentEdge.Start == current ? currentEdge.End : currentEdge.Start;
                    currentEdge.Color = Color.Yellow;
                    if (!nextNode.Visited)
                    {
                        if (!nextNode.Calculated || nextNode.Distance > (current.Distance + currentEdge.Value))
                        {
                            nextNode.Priority = (current.Distance + currentEdge.Value) + heuristic(nextNode, end) ;
                            nextNode.Distance = (current.Distance + currentEdge.Value);
                            nextNode.From = current;
                            nextNode.Traveled = currentEdge;

                            if (nextNode.Calculated)
                            {
                                queue.UpdatePriority(nextNode, nextNode.Priority);
                            }
                            else
                            {
                                queue.Enqueue(nextNode, nextNode.Priority);
                                nextNode.Calculated = true;
                                nextNode.Color = Color.Pink;
                            }
                        }
                    }

                }
            }
            return new List<Vector2D>();
        }
        public static IEnumerable<Vector2D> Dijkstra(Graph<Vector2D> graph, GraphNode<Vector2D> start, GraphNode<Vector2D> end)
            => AStar(graph, start, end, heuristic: noHeuristic);

        public static IEnumerable<Vector2D> AStar(VectorGraph graph, Vector2D start, Vector2D end, Func<GraphNode<Vector2D>, GraphNode<Vector2D>, float> heuristic)
        {
            var closestS = FindClosestNode(graph, start);
            yield return closestS.Data;
            var closestE = FindClosestNode(graph, end);

            var r = AStar(graph, closestS, closestE, heuristic);
            foreach (var vec in r)
            {
                yield return vec;
            }
            if(r.Any())
                yield return end;
        }
    }
}
