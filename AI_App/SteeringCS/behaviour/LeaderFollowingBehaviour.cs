using System.Collections.Generic;
using System.Linq;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class LeaderFollowingBehaviour : ISteeringBehaviour<IFollower>
    {
        public IFollower ME { get; set; }
        private const double LeaderBehindDist = 30;
        public LeaderFollowingBehaviour(IFollower me)
        {
            ME = me;
        }
        public Vector2D Calculate()
        {
            var group = ME.Neighbors;
            var groupL = group as List<IMover> ?? group.ToList();

            var tv = ME.Leader.Velocity * -1;
            if(!tv.LenghtIsZero())
                tv = tv.Normalize() * LeaderBehindDist;
            var behind = ME.Leader.Pos + tv;

            var arriveForce = Arrive(behind, ME, ME.SlowingRadius) * ME.FollowValue;
            var separationForce = Separation(groupL, ME) * ME.SeparationValue;
            return (arriveForce + separationForce).Truncate(ME.MaxForce);
        }
    }
}