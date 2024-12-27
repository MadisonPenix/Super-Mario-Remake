using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Projectiles
{
    public interface IProjectile : ICollidable
    {
        public int width { get; }
        public int height { get; }

        public void Bounce();
        new void Update(GameTime gameTime);
        new void Draw(SpriteBatch spriteBatch);
    }
}
