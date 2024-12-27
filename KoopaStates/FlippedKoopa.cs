using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class FlippedKoopaState : IKoopaStateMachine
    {
        private Koopa koopa;
        public ISprite sprite { get; private set; }

        public FlippedKoopaState(Koopa koopa)
        {
            this.koopa = koopa;
            this.sprite = SpriteFactory.Instance.CreateFlippedKoopaSprite();
        }

        public void ChangeDirection()
        {
            //do nothing, koopa is flipped
        }

        public void BeStomped()
        {
            //do nothing, koopa is already flipped
        }

        public void BeFlipped()
        {
            //do nothing, koopa is already flipped
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        public void Update(GameTime gameTime)
        {
            //do nothing, koopa is already flipped
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, koopa.Position);
        }
    }
}
