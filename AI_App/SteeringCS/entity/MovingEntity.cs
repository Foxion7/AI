using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.behaviour;

namespace SteeringCS.entity
{

    abstract class MovingEntity : BaseGameEntity
    {
        public Vector2D Velocity { get; set; }
        public float Mass { get; set; }
        public float MaxSpeed { get; set; }
        public BaseGameEntity Target { get; set; }
        public SteeringBehaviour SB { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 30;
            MaxSpeed = 30;
            Velocity = new Vector2D();
            SB = new ArriveBehaviour(this);
        }

        public override void Update(float timeElapsed)
        {
            if (Target != null)
            {
                //calculate the combined force from each steering behavior in the vehicle's list
                Vector2D steeringForce = SB.Calculate();

                //Acceleration = Force/Mass
                Vector2D acceleration = steeringForce.Divide(Mass);

                //update velocity
                Velocity = Velocity.Add(acceleration.Multiply(timeElapsed));

                //make sure vehicle does not exceed maximum velocity
                Velocity = Velocity.Truncate(MaxSpeed);

                //update the position
                Pos = Pos.Add(Velocity.Multiply(timeElapsed));

                //update the heading if the vehicle has a non zero velocity
                if (Velocity.LengthSquared() > 0.00000001)
                {
                    //m_vHeading = Vec2DNormalize(m_vVelocity);
                    //m_vSide = m_vHeading.Perp();
                }
            }
            else if(Velocity.LengthSquared() > 0.00000001)
            {
                Velocity = Velocity.Multiply(0);
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
