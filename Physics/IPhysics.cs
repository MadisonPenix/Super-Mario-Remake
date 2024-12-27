using Microsoft.Xna.Framework;

namespace TeamMilkGame.Physics
{
    public interface IPhysics
    {
        public double Xvelocity { get; set; }
        public double Yvelocity { get; set; }
        public double Xacceleration { get; set; }
        public double Yacceleration { get; set; }
        void Update(GameTime time, double maxXSpeed = -1, double maxYSpeed = -1);

        void AddForce(Vector2 force);

        void SetVelocity(Vector2 velocity);

        void SetGravity(bool useGravity);
    }
}
