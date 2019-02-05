using System;
using System.Collections.Generic;
using System.Linq;

namespace SteeringCS.util
{
    public class ExplorationArea
    {
        private Random _rnd = new Random();
        private HashSet<(int x, int y)> _space = new HashSet<(int x, int y)>();
        private (int x, int y) _current;
        private bool _last = false;
        private bool _done = false;
        private int _width;
        private int _height;
        private int _precision;

        public ExplorationArea(int width, int height, int precision, Vector2D start)
        {
            _width = width;
            _height = height;
            _precision = precision;

            for (int i = 0; i < width; i += precision)
            {
                for (int j = 0; j < height; j += precision)
                {
                    _space.Add((i,j));
                }
            }

            var modX = (int)start.X % precision;
            var modY = (int)start.Y % precision;
            int startX =  (int)start.X - modX;
            int startY =  (int)start.Y - modY;
            _current = (startX, startY);
        }

        public Vector2D CurrentWaypoint()
            => new Vector2D(_current.x, _current.y);


        //Are there no WayPoints left
        public bool Done()
        {
            if (_done == false)
            {
                _done = !_space.Any();
                return _last;
            }
            else
                return _done;
        }
        //Are there no WayPoints left
        public bool Last()
        {
            if (_done)
                return true;
            else if (_last == false)
            {
                _last = _space.Take(2).Count() == 1;
                return _last;
            }
            else
                return true;
        }


        public void SetNextWaypoint()
        {
            if (_done)
            {

            }
            else if (_last)
            {
                _space.Remove(_current);
                _done = true;
                return;
            }
            else
            {
                _space.Remove(_current);
                //_current = _space.
            }

        }
    }
}