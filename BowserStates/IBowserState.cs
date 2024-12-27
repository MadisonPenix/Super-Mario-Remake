using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public interface IBowserState
    {
        public ISprite sprite { get; }
        void ChangeDirection();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
