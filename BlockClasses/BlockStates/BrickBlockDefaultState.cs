using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class BrickBlockDefaultState : IBlockStateInteractable
    {
        private BrickBlock brickBlock;
        private ISprite sprite;

        public BrickBlockDefaultState(BrickBlock brickBlock)
        {
            this.brickBlock = brickBlock;
            sprite = SpriteFactory.Instance.CreateBrickBlockDefaultSprite();
        }
        public void Activate()
        {
            // code to break block/replace with broken block animation in future implementation
            // brickBlock.state = new BrickBlockBrokenState(brickBlock);
            return;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            sprite.Draw(spriteBatch, position);
        }

        public void Update()
        {

        }
    }
}
