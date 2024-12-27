using Microsoft.Xna.Framework;
using System;

namespace TeamMilkGame.Physics
{
    internal class PhysicsHandler : IPhysics
    {
        private const double GRAVITY_ACCELERATION = -1200;
        private IPhysicsObject physicsObj;
        private bool gravity;

        //velocity in pixels/s
        public double Xvelocity { get; set; }
        public double Yvelocity { get; set; }

        //acceleration in pixels/s^2
        public double Yacceleration { get; set; }
        public double Xacceleration { get; set; }

        public double maxXSpeed { get; private set; }
        public double maxYSpeed { get; private set; }

        private bool useMaxSpeed;
        public PhysicsHandler(IPhysicsObject obj, bool useGravity = true, bool useMaxSpeed = false, double maxXSpeed = -1, double maxYSpeed = -1)
        {
            Yvelocity = 0;
            Xvelocity = 0;
            Xacceleration = 0;
            Yacceleration = 0;
            this.physicsObj = obj;
            this.gravity = useGravity;
            this.useMaxSpeed = useMaxSpeed;
            this.maxXSpeed = maxXSpeed;
            this.maxYSpeed = maxYSpeed;
        }

        public void Update(GameTime time, double maxXSpeed = -1, double maxYSpeed = -1)
        {
            if (maxXSpeed > -1)
            {
                this.maxXSpeed = maxXSpeed;
            }
            if (maxYSpeed > -1)
            {
                this.maxYSpeed = maxYSpeed;
            }
            double deltaTime = time.ElapsedGameTime.TotalSeconds;
            //update gravity
            UpdateVelocities(deltaTime);

            UpdatePosition(deltaTime);

            //resets forces applied (used to have forces be applied per different frames when disired)
            Yacceleration = 0;
            Xacceleration = 0;
        }

        private void UpdateVelocities(double deltaTime)
        {
            if (gravity)
            {
                Yvelocity += (GRAVITY_ACCELERATION + Yacceleration) * deltaTime;
            }
            else
            {
                Yvelocity += Yacceleration * deltaTime;
            }
            Xvelocity += Xacceleration * deltaTime;

            //clamps speed if used
            if (useMaxSpeed)
            {
                //clamps speed if specified
                if (maxXSpeed > -1)
                {
                    Xvelocity = Math.Clamp(Xvelocity, -maxXSpeed, maxXSpeed);
                }
                if (maxYSpeed > -1)
                {
                    Yvelocity = Math.Clamp(Yvelocity, -maxYSpeed, maxYSpeed);
                }
            }
        }

        //updates object's position based on forces applied
        private void UpdatePosition(double deltaTime)
        {
            //calculates next position from forces/velocity (x = x0 + v0t + .5 * a * t^2)
            float xPos = (float)(physicsObj.Position.X + Xvelocity * deltaTime);
            float yPos = (float)(physicsObj.Position.Y + -Yvelocity * deltaTime);
            this.physicsObj.Position = new Vector2(xPos, yPos);
        }

        //adds force (acceleration) to object for 1 frame
        public void AddForce(Vector2 force)
        {
            Xacceleration = force.X;
            Yacceleration = force.Y;
        }

        //sets objects velocities
        public void SetVelocity(Vector2 velocity)
        {
            Xvelocity = velocity.X;
            Yvelocity = velocity.Y;
        }

        //toggles gravity on and off
        public void SetGravity(bool useGravity)
        {
            this.gravity = useGravity;
        }

        public void SetMaxSpeed(Vector2 maxSpeed)
        {
            maxXSpeed = maxSpeed.X;
            maxYSpeed = maxSpeed.Y;
        }
    }
}
