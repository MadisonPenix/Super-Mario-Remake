using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public abstract class IComponent
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void DrawWithColor(GameTime gameTime, SpriteBatch spriteBatch, Color color);

        public abstract void Update(GameTime gameTime);
    }
}
