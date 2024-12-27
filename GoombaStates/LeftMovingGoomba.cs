using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public class LeftMovingGoombaState : IGoombaState
    {
        private Goomba goomba;
        public ISprite sprite { get; private set; }

        public LeftMovingGoombaState(Goomba goomba)
        {
            this.goomba = goomba;
            this.sprite = SpriteFactory.Instance.CreateMovingGoombaSprite();
        }

        public void ChangeDirection()
        {
            goomba.state = new RightMovingGoombaState(goomba);
        }

        public void BeStomped()
        {
            goomba.state = new StompedGoombaState(goomba);
        }

        public void BeFlipped()
        {
            goomba.state = new FlippedGoombaState(goomba);
        }

        public void Update(GameTime gameTime)
        {
            goomba.Position -= new Vector2(2, 0);
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, goomba.Position);
        }
    }
}
