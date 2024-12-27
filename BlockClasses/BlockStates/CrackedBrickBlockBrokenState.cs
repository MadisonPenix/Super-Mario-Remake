using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class CrackedBrickBlockBrokenState : IBlockStateInactive
    {
        private CrackedBrickBlock cBlockBroken;
        private ISprite sprite;

        public CrackedBrickBlockBrokenState(CrackedBrickBlock cBlockBroken)
        {
            this.cBlockBroken = cBlockBroken;
            sprite = SpriteFactory.Instance.CreateCrackedBrickBlockBrokenSprite();
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
