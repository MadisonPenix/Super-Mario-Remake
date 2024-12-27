using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.BlockClasses.BlockStates
{
    public interface IBlockStateInteractable
    {
        void Activate(); // block performs action/state changes -> ? block produces item, both brick blocks break, pipe activates
        void Draw(SpriteBatch spriteBatch, Vector2 position);
        void Update();
    }
}
