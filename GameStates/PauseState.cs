using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
//contemplate creating a button in the top left corner of the screen to pause and unpause the game
namespace TeamMilkGame.GameStates
{
    public class PauseState : State
    {
        /*
         * paused menu variables
         */
        private bool paused;
        private Controller controlls;
        private SpriteFont buttonFont;
        private Texture2D playTexture;
        private List<IComponent> components;
        private Vector2 viewportCenter;
        //needs to be able to be used by other methods
        private Button pauseGameButton;
        public PauseState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //pause menu sprites
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            playTexture = content.Load<Texture2D>("playButton");

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            //viewportCenter = new Vector2(camera.Viewport.Width / 2f, camera.Viewport.Height / 2f);
            int textHeight = (int)buttonFont.MeasureString(GameUtility.Instance.MENU_STR).Y;


            /*
             * pause menu buttons
             */
            Button resumeGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.RESUME_STR).X / 2,
                    viewportCenter.Y - textHeight / 2),
                buttonText = GameUtility.Instance.RESUME_STR,
            };

            resumeGameButton.Click += ResumeGameButton_Click;

            Button menuGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.MENU_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.MENU_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.MENU_STR,
            };

            menuGameButton.Click += MenuGameButton_Click;

            Button quitGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.QUIT_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.QUIT_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.QUIT_STR,
            };

            quitGameButton.Click += QuitGameButton_Click;

            //the button in the top left corner of the screen to pause and unpause
            pauseGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(2, 2),
                buttonText = "  ", //no text, but needs a bounding box
            };
            pauseGameButton.Click += PauseGameButton_Click;

            components = new List<IComponent>()
            {
                resumeGameButton,
                menuGameButton,
                quitGameButton,
                pauseGameButton,
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //get the center of the screen
            Vector2 viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            //set a transluscency color for the pause/play buttons
            Color buttonColor = GameUtility.Instance.BUTTON_COLOR;

            spriteBatch.Begin();
            spriteBatch.Draw(playTexture, GameUtility.Instance.ALT_SPRITEBATCH_RECT, buttonColor);

            //draw the paused overlay

            //draw a paused title (might make font bigger for this later)
            spriteBatch.DrawString(buttonFont, GameUtility.Instance.PAUSE_STR,
                viewportCenter - buttonFont.MeasureString(GameUtility.Instance.PAUSE_STR) / 2 - GameUtility.Instance.PAUSE_TEXT_OFFSET,
                Color.White);
            //draw the buttons
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();


        }


        private void ResumeGameButton_Click(object sender, EventArgs e)
        {
            //reverse the paused bool; unpause the game
            game.paused = !game.paused;
        }
        private void MenuGameButton_Click(object sender, EventArgs e)
        {
            //change the state of the game to MenuState
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }
        private void PauseGameButton_Click(object sender, EventArgs e)
        {
            
                game.paused = false;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
            //update paused
            this.paused = game.paused;
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            //exit the game
            game.Exit();
        }
    }
}
