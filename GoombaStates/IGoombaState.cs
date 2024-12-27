using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public interface IGoombaState
    {
        public ISprite sprite { get; }
        void ChangeDirection();
        void BeStomped();
        void BeFlipped();
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
