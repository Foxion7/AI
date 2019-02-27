using System.Collections.Generic;
using System.Linq;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class LeaderFollowingBehaviour : ISteeringBehaviour
    {
        private IGrouper _me;
        public IMover Leader { get; set; }
        public double LeaderBehindDist { get; set; }
        public double SlowingRadius { get; set; }
        public double GroupValue { get; set; }
        public double FollowValue { get; set; }
        public double SeparationValue { get; set; }

        public LeaderFollowingBehaviour(IGrouper me, IMover leader, double slowingRadius, double leaderBehindDist, double groupValue, double followValue=1, double separationValue=1)
        {
            _me = me;
            Leader = leader;
            SlowingRadius = slowingRadius;
            LeaderBehindDist = leaderBehindDist;
            GroupValue = groupValue;
            FollowValue = followValue;
            SeparationValue = separationValue;
        }


        public Vector2D Calculate()
        {
            if (Leader == null)
                return new Vector2D();

            var group = _me.Neighbors;
            var groupL = group as List<IGrouper> ?? group.ToList();

            var tv = Leader.Velocity * -1;
            if(!tv.LenghtIsZero())
                tv = tv.Normalize() * LeaderBehindDist;
            var behind = Leader.Pos + tv;

            var followForce = Follow(behind, _me, SlowingRadius)*FollowValue;
            var separationForce = Separation(groupL, _me) * SeparationValue;
            var desiredSpeed = followForce + separationForce;
            if (!desiredSpeed.LenghtIsZero())
                desiredSpeed = desiredSpeed.Normalize() * GroupValue;
            return (desiredSpeed - _me.Velocity).Truncate(_me.MaxForce);
        }
    }
}