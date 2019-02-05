using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS
{
    public interface ISteeringBehaviour
    {
        Vector2D Calculate();
    }

    
}
