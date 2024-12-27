using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public interface ISprite
    {
        public int spriteWidth { get; }
        public int spriteHeight { get; }

        public Texture2D texture { get; }
        public void Update(GameTime time);

        public void Draw(SpriteBatch spriteBatch, Vector2 position);
        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale);

        public void DrawWithColor(SpriteBatch spriteBatch, Vector2 position, Color color);
    }
}
