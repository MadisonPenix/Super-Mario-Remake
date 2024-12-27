using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Sprites;

namespace TeamMilkGame
{
    public class Background : IGameObject
    {
        public Texture2D backgroundTexture;
        public readonly string levelName;
        public ISprite sprite { get; private set; }
        public readonly string levelFileName;
        public HUD levelHUD;
        private double levelTimer;
        public Vector2 Position { get; set; }
        public Background(int x, int y)
        {
            Position = new Vector2(x, y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite = new NonAnimatedSprite(backgroundTexture, Position, new Vector2(backgroundTexture.Width, backgroundTexture.Height));
            sprite.Draw(spriteBatch, Position);
        }

        public void Update(GameTime gameTime)
        {
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Background((int)Position.X, (int)Position.Y);
        }
    }
}
