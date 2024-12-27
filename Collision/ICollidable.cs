using Microsoft.Xna.Framework;

namespace TeamMilkGame.Collision
{
    public interface ICollidable : IGameObject
    {
        public enum CollisionType
        {
            Mario,
            SuperMario,
            Enemy,
            NonMovingShellKoopa,
            Block,
            Projectile,
            FireFlower,
            Coin,
            ExtraLife,
            Star,
            Mushroom,
            Flagpole,
            StarMario,
            Pipe
        }
        public Rectangle BoundingBox { get; set; }
        public CollisionType GetCollisionType();

        public void UpdateBoundingBox();
    }
}
