using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class ShellKoopaStateMachine : IKoopaStateMachine
    {
        private Koopa koopa;
        public ISprite sprite { get; private set; }
        public bool facingLeft { get; set; }
        private bool moving = false;

        public ShellKoopaStateMachine(Koopa koopa)
        {
            this.koopa = koopa;
            this.sprite = SpriteFactory.Instance.CreateStompedKoopaSprite();
        }

        public void ChangeDirection()
        {
            facingLeft = !facingLeft;
        }

        public void BeStomped()
        {
            koopa.health--;
            if (!moving) { moving = true; }
            else { BeFlipped(); }
        }

        public void BeFlipped()
        {
            koopa.stateMachine = new FlippedKoopaState(koopa);
        }

        public CollisionType GetCollisionType()
        {
            if (moving)
            {
                return ICollidable.CollisionType.Enemy;
            }
            else
            {
                return ICollidable.CollisionType.NonMovingShellKoopa;
            }
        }

        public void Update(GameTime gameTime)
        {
            //should maybe use a speed variable instead of moving certain number of pixels per frame
            if (moving)
            {
                if (facingLeft)
                {
                    koopa.Position -= new Vector2(5, 0);
                }
                else
                {
                    koopa.Position += new Vector2(5, 0);
                }
            }
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, koopa.Position);
        }
    }
}
