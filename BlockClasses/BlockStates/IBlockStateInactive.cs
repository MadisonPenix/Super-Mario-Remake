using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public interface IBlockStateInactive
    {
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update();
    }
}
