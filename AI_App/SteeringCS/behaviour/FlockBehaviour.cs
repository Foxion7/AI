using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using static SteeringCS.behaviour.StaticBehaviours;

namespace SteeringCS.behaviour
{
    class FlockBehaviour : ISteeringBehaviour<IFlocker> 
    {
        public IFlocker ME { get; set; }
        public FlockBehaviour(IFlocker me)
        {
            ME = me;
        }


        public Vector2D Calculate()
        {
            var group = ME.Neighbors.ToList();
            if (group.Count <= 1)
                return new Vector2D();


            var separationForce =  Separation(group, ME) * ME.SeparationValue;
            var alignmentForce = Alignment(group)        * ME.AlignmentValue;
            var cohesionForce = Cohesion(group, ME)      * ME.CohesionValue;
            var desiredSpeed = (separationForce + alignmentForce + cohesionForce).Normalize()*ME.GroupValue;
            if (desiredSpeed.LenghtIsZero())
                return desiredSpeed;

            var neededForce = desiredSpeed - ME.Velocity;
            return neededForce.Truncate(ME.MaxForce);
        }
    }
}
