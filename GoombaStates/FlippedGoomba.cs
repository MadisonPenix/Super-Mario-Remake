using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public class FlippedGoombaState : IGoombaState
    {
        private Goomba goomba;
        public ISprite sprite { get; private set; }

        public FlippedGoombaState(Goomba goomba)
        {
            this.goomba = goomba;
            this.sprite = SpriteFactory.Instance.CreateFlippedGoombaSprite();
        }

        public void ChangeDirection()
        {
            //do nothing, goomba is flipped
        }

        public void BeStomped()
        {
            //do nothing, goomba is already flipped
        }

        public void BeFlipped()
        {
            //do nothing, goomba is already flipped
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, goomba.Position);
        }
    }
}
