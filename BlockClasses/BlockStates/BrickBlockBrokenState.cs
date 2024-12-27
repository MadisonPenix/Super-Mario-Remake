using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class BrickBlockBrokenState : IBlockStateInactive
    {
        private BrickBlock bBlockBroken;
        private ISprite sprite;

        public BrickBlockBrokenState(BrickBlock bBlockBroken)
        {
            this.bBlockBroken = bBlockBroken;
            sprite = SpriteFactory.Instance.CreateBrickBlockBrokenSprite();
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
