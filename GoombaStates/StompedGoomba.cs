using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public class StompedGoombaState : IGoombaState
    {
        private Goomba goomba;
        public ISprite sprite { get; private set; }

        public StompedGoombaState(Goomba goomba)
        {
            this.goomba = goomba;
            this.sprite = SpriteFactory.Instance.CreateStompedGoombaSprite();
        }

        public void ChangeDirection()
        {
            //do nothing, goomba is stomped
        }

        public void BeStomped()
        {
            //do nothing, goomba is already stomped
        }

        public void BeFlipped()
        {
            //do nothing, goomba is already stomped
        }

        public void Update(GameTime gameTime)
        {
            goomba.timer--;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, goomba.Position);
        }
    }
}
