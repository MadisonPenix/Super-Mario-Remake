using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class QuestionBlockInactiveState : IBlockStateInactive
    {
        private QuestionBlockInactive qBlockInactive;
        private ISprite sprite;

        public QuestionBlockInactiveState(QuestionBlockInactive qBlockInactive)
        {
            this.qBlockInactive = qBlockInactive;
            sprite = SpriteFactory.Instance.CreateQuestionBlockActiveSprite();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(spriteBatch, qBlockInactive.Position);
        }

        public void Update()
        {

        }
    }
}
