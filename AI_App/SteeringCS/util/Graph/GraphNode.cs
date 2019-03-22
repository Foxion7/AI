using System.Collections.Generic;
using System.Drawing;
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
        public bool Visited { get; set; } = false;
        public bool Calculated { get; set; } = false;
        public float Distance { get; set; }
        public new float Priority { get; set; }
        public GraphNode<T> From { get; set; }
        public GraphEdge<T> Traveled { get; set; }
        public Color Color { get; set; } = Color.Black;
        
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