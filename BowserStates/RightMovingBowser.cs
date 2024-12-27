using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public class RightMovingBowserState : IBowserState
    {
        private Bowser bowser;
        public ISprite sprite { get; private set; }

        public RightMovingBowserState(Bowser bowser)
        {
            this.bowser = bowser;
            this.sprite = SpriteFactory.Instance.CreateRightMovingBowserSprite();
        }

        public void ChangeDirection()
        {
            bowser.faceLeft = true;
            bowser.state = new LeftMovingBowserState(bowser);
        }

        public void Update(GameTime gameTime)
        {
            bowser.Position += new Vector2(2, 0);
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, bowser.Position);
        }
    }
}
