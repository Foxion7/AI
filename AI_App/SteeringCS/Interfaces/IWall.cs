using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.Interfaces
{
    public interface IWall : IEntity
    {
        double Width { get; set; }
        double Height { get; set; }
        Vector2D Center { get; set; }
    }
}
