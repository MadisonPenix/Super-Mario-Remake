using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.States
{
    public abstract class State
    {
        protected ContentManager content;

        protected GraphicsDevice graphicsDevice;

        protected MilkGame game;
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);


        public State(MilkGame Game, GraphicsDevice GraphicsDevice, ContentManager Content)
        {
            game = Game;

            graphicsDevice = GraphicsDevice;

            content = Content;
        }

        public abstract void Update(GameTime gameTime);
    }
}
