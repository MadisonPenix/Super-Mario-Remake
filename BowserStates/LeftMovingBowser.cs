using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public class LeftMovingBowserState : IBowserState
    {
        private Bowser bowser;
        public ISprite sprite { get; private set; }

        public LeftMovingBowserState(Bowser bowser)
        {
            this.bowser = bowser;
            this.sprite = SpriteFactory.Instance.CreateLeftMovingBowserSprite();
        }

        public void ChangeDirection()
        {
            bowser.faceLeft = false;
            bowser.state = new RightMovingBowserState(bowser);
        }

        public void Update(GameTime gameTime)
        {
            bowser.Position -= new Vector2(2, 0);
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, bowser.Position);
        }
    }
}
