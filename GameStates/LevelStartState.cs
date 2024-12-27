using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamMilkGame.Commands;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class LevelStartState : State
    {
        private Texture2D marioSprite;
        private Texture2D luigiSprite;
        private Vector2 viewportCenter;
        private SpriteFont buttonFont;
        private double timer;
        private List<int> lives;
        private int level;
        private Rectangle marioSource;

        public LevelStartState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //sprite loading
            // NOTE: Should be using ISprite instead
            // To-Do: Replace with ISprite
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            marioSprite = content.Load<Texture2D>("mario");
            luigiSprite = content.Load<Texture2D>("luigi");
            marioSource = GameUtility.Instance.MARIO_SOURCE;

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            timer = GameUtility.Instance.LEVEL_DELAY_TIMER; //amount of time we want this display to show for

            /*TEMPORARY until lives and levels are implemented*/
            lives = new List<int>();
            foreach(IMario player in GameObjectManager.Instance.Players)
            {
                lives.Add(player.lives.GetLives());
            }
            level = 1;

            //reset the game
            //ICommands reset = new ResetCommand(mario, game);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //draw a black background
            graphicsDevice.Clear(Color.Black);
            //display the level number
            spriteBatch.DrawString(buttonFont, GameUtility.Instance.LEVEL_STR + level,
                    viewportCenter - buttonFont.MeasureString(GameUtility.Instance.LEVEL_STR + level) / 2 - GameUtility.Instance.PAUSE_TEXT_OFFSET, Color.White);

            //display mario sprite
            Vector2 sizePoint = new Vector2(marioSource.Width * GameUtility.Instance.TITLE_MULT, marioSource.Height * GameUtility.Instance.TITLE_MULT);
            int i = 0;
            foreach (int playerLives in lives)
            {
                Texture2D sprite;
                if(i % 2 == 0)
                {
                    sprite = marioSprite;
                }
                else
                {
                    sprite = luigiSprite;
                }
                Vector2 locationPoint = new Vector2(viewportCenter.X - buttonFont.MeasureString(" X ").X, (viewportCenter.Y - buttonFont.MeasureString(" X ").Y / 2) + (i * 125));
                spriteBatch.Draw(
                sprite,
                //sets mario to be in line vertically with the lives text. The combination of these two is centered horizontally
                new Rectangle(locationPoint.ToPoint(), sizePoint.ToPoint()),
                marioSource, Color.White
                );

                //display mario's lives
                Vector2 loc = viewportCenter - buttonFont.MeasureString(" X ") / 2;
                spriteBatch.DrawString(buttonFont, " X " + playerLives,
                    new Vector2(loc.X, loc.Y + (i * 125)), Color.White);
                i++;
            }

            /* TO-DO:
             * Create check for whether we are in single player or multiplayer - display luigi
             */

            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            //remember timer = 2.5
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            //switch to the game
            if (timer <= 0)
            {
                game.ChangeState(new GameState(game, graphicsDevice, content));
            }
        }
    }
}
