using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class NonShellKoopaStateMachine : IKoopaStateMachine
    {
        private Koopa koopa;
        public ISprite sprite { get; private set; }
        public bool facingLeft { get; set; }

        public NonShellKoopaStateMachine(Koopa koopa)
        {
            facingLeft = false;
            this.koopa = koopa;
            this.sprite = SpriteFactory.Instance.CreateLeftMovingKoopaSprite();
        }

        public void ChangeDirection()
        {
            facingLeft = !facingLeft;
            if (facingLeft)
            {
                this.sprite = SpriteFactory.Instance.CreateLeftMovingKoopaSprite();
            }
            else
            {
                this.sprite = SpriteFactory.Instance.CreateRightMovingKoopaSprite();
            }
        }

        public void BeStomped()
        {
            koopa.health--;
            koopa.stateMachine = new ShellKoopaStateMachine(koopa);
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Enemy;
        }

        public void BeFlipped()
        {
            koopa.stateMachine = new FlippedKoopaState(koopa);
        }

        public void Update(GameTime gameTime)
        {
            if (facingLeft)
            {
                koopa.Position -= new Vector2(1, 0);
            }
            else
            {
                koopa.Position += new Vector2(1, 0);
            }
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, koopa.Position);
        }
    }
}
