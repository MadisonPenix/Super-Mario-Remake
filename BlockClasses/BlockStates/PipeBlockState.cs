using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class PipeBlockState : IBlockStateInteractable
    {
        private PipeBlock pBlock;
        private ISprite sprite;

        public PipeBlockState(PipeBlock pBlock)
        {
            this.pBlock = pBlock;
            sprite = SpriteFactory.Instance.CreatePipeBlockSprite();
        }

        public void Activate()
        {
            // code to make pipe traversable in future implementations
            return;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(spriteBatch, pBlock.Position);
        }

        public void Update()
        {

        }
    }
}
