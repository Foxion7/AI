using System.Collections.Generic;
using System.Linq;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    public class LeaderFollowingBehaviour : ISteeringBehaviour<IFollower>
    {
        public IFollower ME { get; set; }
        private const double LEADER_BEHIND_DIST = 30;
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
                tv = tv.Normalize() * LEADER_BEHIND_DIST;
            var behind = ME.Leader.Pos + tv;

            var seekForce = Seek(behind, ME) * ME.FollowValue;
            var separationForce = Separation(groupL, ME) * ME.SeparationValue;
            return (seekForce + separationForce).Truncate(ME.MaxForce);
        }
    }

    public interface IFollower : IGrouper<IMover>
    {
        MovingEntity Leader { get; }
        double SeparationValue { get; }
        double FollowValue { get; }
        double AvoidValue { get; }
    }
}