using System.Collections.Generic;
using System.Linq;

namespace SteeringCS.util.Graph
{
    //everything in this class assumes two way edges.
    public class Graph<T>
    {
        private List<GraphEdge<T>> _edges;
        public IEnumerable<GraphEdge<T>> Edges => _edges;

        private Dictionary<T, GraphNode<T>> _nodes;
        public IEnumerable<GraphNode<T>> Nodes => _nodes.Values;


        public Graph()
        {
            _edges = new List<GraphEdge<T>>();
            _nodes = new Dictionary<T, GraphNode<T>>();
        }




        public GraphNode<T> AddNode(T data)
        {
            var node = new GraphNode<T>(data);
            _nodes.Add(data, node);
            return node;
        }
        public GraphNode<T> AddNode(GraphNode<T> node)
        {
            _nodes.Add(node.Data, node);
            return node;
        }

        /// <summary>
        /// this will only work if both values are already in the graph, it won't! add them.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns>the created edge, if it exist, otherwise null</returns>
        public GraphEdge<T> createEdge(T start, T end, float value = 1)
        {

            if (_nodes[start] != null && _nodes[end] != null)
            {
                var edge = new GraphEdge<T>(_nodes[start],_nodes[end], value);
                _nodes[start].AddEdgeTwoWay(edge);
                _nodes[end]  .AddEdgeTwoWay(edge);
                _edges.Add(edge);
                return edge;
            }

            return null;
        }

        /// <summary>
        /// this will only work if both nodes are already in the graph, it won't! add them.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="value"></param>
        /// <returns>the edge if it is created, null otherwise</returns>
        public GraphEdge<T> createEdge(GraphNode<T> start, GraphNode<T> end, float value = 1)
        {
            if (start != null && end != null)
            {
                var edge = new GraphEdge<T>(start, end, value);
                start .AddEdgeTwoWay(edge);
                end   .AddEdgeTwoWay(edge);
                _edges.Add(edge);
                return edge;
            }

            return null;
        }

    }
}