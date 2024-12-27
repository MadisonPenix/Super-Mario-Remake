using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public interface IGameObject
    {
        public ISprite sprite { get; }
        public Vector2 Position { get; set; }
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
        public IGameObject Clone(Vector2 location);
    }
}
