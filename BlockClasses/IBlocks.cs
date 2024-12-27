using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;

namespace TeamMilkGame
{
    public interface IBlocks : ICollidable
    {
        public bool Interactable { get; }

        public int width { get; }
        public int height { get; }

        new void Update(GameTime gameTime);
        new void Draw(SpriteBatch spriteBatch);

        public void Bounce();
    }
}