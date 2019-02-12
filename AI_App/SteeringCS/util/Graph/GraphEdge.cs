using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.Graph
{
    public class GraphEdge<T>
    {
        public GraphNode<T> Start { get; }
        public GraphNode<T> End { get; }

        //this is the distance
        public float Value { get; }

        public GraphEdge(GraphNode<T> start, GraphNode<T> end, float value = 1)
        {
            Start = start;
            End = end;
            Value = value;
        }
    }
}