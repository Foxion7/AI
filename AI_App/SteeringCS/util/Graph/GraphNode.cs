using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Priority_Queue;

namespace SteeringCS.util.Graph
{
    //everything in this class assumes two way edges.
    public class GraphNode<T> : FastPriorityQueueNode
    {


        public readonly T Data;
        private List<GraphEdge<T>> _edges;
        public IEnumerable<GraphEdge<T>> Edges => _edges;
        public bool Seen { get; set; } = false;
        public bool ShallowSeen { get; set; } = false;
        public new float Priority { get; set; }
        public GraphNode<T> From { get; set; }

        public GraphNode(T data, params GraphEdge<T>[] edges)
        {
            Data = data;
            _edges = edges.ToList() ?? new List<GraphEdge<T>>();
        }

        //if you want an undirected graph use this
        public void AddEdgeTwoWay(GraphEdge<T> edge)
        {
            if (edge.Start == this || edge.End == this)
                _edges.Add(edge);
        }

        //if you want an directed graph use this
        public void AddEdgeOneWay(GraphEdge<T> edge)
        {
            if (edge.Start == this)
                _edges.Add(edge);
        }

    }
}