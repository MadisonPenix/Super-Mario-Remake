using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class CrackedBrickBlockDefaultState : IBlockStateInteractable
    {
        private CrackedBrickBlock cBlock;
        private ISprite sprite;

        public CrackedBrickBlockDefaultState(CrackedBrickBlock cBlock)
        {
            this.cBlock = cBlock;
            sprite = SpriteFactory.Instance.CreateCrackedBrickBlockDefaultSprite();
        }

        public void Activate()
        {
            // code to break block/replace with broken state in future implementation
            // cBlock.state = new CrackedBrickBlockBrokenState(cBlock);
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
