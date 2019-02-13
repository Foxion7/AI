using System;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;
using SteeringCS.util;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class FollowPathBehaviour : ISteeringBehaviour
    {
        private IMover _me;
        public Route Path { get; set; }
        public double WaypointSeekDistSq { get; set; }
        public double SlowingRadius { get; set; }

        public FollowPathBehaviour(IMover me, Route path, double waypointSeekDistSq, double slowingRadius)
        {
            _me = me;
            Path = path;
            WaypointSeekDistSq = waypointSeekDistSq;
            SlowingRadius = slowingRadius;
        }

        public Vector2D Calculate()
        {
            if (Path == null || _me == null)
                return new Vector2D();
            //move to next target if close enough to current target (working in
            //distance squared space)
            if ((Path.CurrentWaypoint() - _me.Pos).LengthSquared() < WaypointSeekDistSq)
                Path.SetNextWaypoint();
            
            if (!Path.Last())
                return Seek(Path.CurrentWaypoint(), _me);
            else
                return Arrive(Path.CurrentWaypoint(), _me, SlowingRadius);
            
        }
    }
}
