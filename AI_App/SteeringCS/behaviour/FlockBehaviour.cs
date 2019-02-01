﻿using System;
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
            var group = ME.Neighbors.Where(gob =>
            {
                var x = gob.Pos.X;
                var y = gob.Pos.Y;
                var maxX = ME.Pos.X + ME.NeighborsRange;
                var minX = ME.Pos.X - ME.NeighborsRange;
                var maxY = ME.Pos.X + ME.NeighborsRange;
                var minY = ME.Pos.X - ME.NeighborsRange;
                return x > minX && x < maxX && y > minY && y < maxY;
            });
            var groupL= group as List<IMover> ?? group.ToList();
            if (!groupL.Any())
                return new Vector2D();


            var separationForce =  Separation(groupL, ME) * ME.SeparationValue;
            var alignmentForce = Alignment(groupL)        * ME.AlignmentValue;
            var cohesionForce = Cohesion(groupL, ME)      * ME.CohesionValue;
            return (separationForce + alignmentForce + cohesionForce).Truncate(ME.MaxForce);
        }
    }
}
