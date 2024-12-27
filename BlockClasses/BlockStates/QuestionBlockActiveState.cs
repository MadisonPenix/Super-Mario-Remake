using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class QuestionBlockActiveState : IBlockStateInteractable
    {
        private QuestionBlock qBlock;
        private ISprite sprite;

        public QuestionBlockActiveState(QuestionBlock qBlock)
        {
            this.qBlock = qBlock;
            sprite = SpriteFactory.Instance.CreateQuestionBlockActiveSprite();
        }
        public void Activate()
        {
            // code to generate stored item in ? block here in future implementation
            // after item comes out, replace with inactive ? block 
            QuestionBlockInactive qBlockInactive = new QuestionBlockInactive((int)qBlock.Position.X, (int)qBlock.Position.Y);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(spriteBatch, qBlock.Position);
        }

        public void Update()
        {

        }
    }
}
