using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public class SolidBlockState : IBlockStateInactive
    {
        private SolidBlock sBlock;
        private ISprite sprite;

        public SolidBlockState(SolidBlock sBlock)
        {
            this.sBlock = sBlock;
            sprite = SpriteFactory.Instance.CreateSolidBlockSprite();
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
