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
        public Vector2D Heading { get; set; }
        public Vector2D Side { get; set; }

        public MovingEntity(Vector2D pos, World w) : base(pos, w)
        {
            Mass = 10;
            MaxSpeed = 30;
            Velocity = new Vector2D();
            SB = new ArriveBehaviour(this, ArriveBehaviour.Deceleration.fast);
            //SB = new SeekBehaviour(this);
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
                    //Heading = Velocity.Normalize();
                    //Side = Heading.Perp();
                }
            }
        }

        public override string ToString()
        {
            return String.Format("{0}", Velocity);
        }
    }
}
