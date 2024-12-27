using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public interface IKoopaStateMachine
    {
        public ISprite sprite { get; }
        void ChangeDirection();
        void BeStomped();
        void BeFlipped();
        public CollisionType GetCollisionType();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
