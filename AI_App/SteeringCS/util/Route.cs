using System.Collections.Generic;

namespace SteeringCS.util
{
    public class Route
    {
        private int _current = 0;
        private List<Vector2D> _waypoints;

        public Route(List<Vector2D> waypoints)
        {
            _waypoints = waypoints;
        }

        public Vector2D CurrentWaypoint()
            => _waypoints[_current];
        

        //is there only one waypoint left.
        public bool Last()
            => _current+1 >= _waypoints.Count;
        

        public void SetNextWaypoint()
        {
            if(_current < _waypoints.Count-1)
                _current++;
        }
    }
}