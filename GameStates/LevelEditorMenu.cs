using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
using TeamMilkGame.XML;
//contemplate creating a button in the top left corner of the screen to pause and unpause the game
namespace TeamMilkGame.GameStates
{
    public class LevelEditorMenu : State
    {
        /*
         * paused menu variables
         */
        private bool paused;
        private Controller controlls;
        private SpriteFont buttonFont;
        private Texture2D pauseTexture;
        private Texture2D playTexture;
        private Texture2D currentTexture;
        private List<IComponent> components;
        private Vector2 viewportCenter;
        //needs to be able to be used by other methods
        private Button pauseGameButton;
        public LevelEditorMenu(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //pause menu sprites
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            pauseTexture = content.Load<Texture2D>("pauseButton");
            playTexture = content.Load<Texture2D>("playButton");
            currentTexture = pauseTexture;

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            //viewportCenter = new Vector2(camera.Viewport.Width / 2f, camera.Viewport.Height / 2f);
            int textHeight = (int)buttonFont.MeasureString(GameUtility.Instance.MENU_STR).Y;


            /*
             * pause menu buttons
             */
            Button saveButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.SAVE_STR).X / 2,
                    viewportCenter.Y - textHeight / 2),
                buttonText = GameUtility.Instance.SAVE_STR,
            };

            saveButton.Click += saveButton_Click;

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

            Button backButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.BACK_STR).X / 2,
                    viewportCenter.Y - textHeight / 2),
                buttonText = GameUtility.Instance.BACK_STR,
            };

            backButton.Click += backButton_Click;


            components = new List<IComponent>()
            {
                saveButton,
                menuGameButton,
                quitGameButton,
                backButton,
            };
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //get the center of the screen
            Vector2 viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            //set a transluscency color for the pause/play buttons
            Color buttonColor = GameUtility.Instance.BUTTON_COLOR;
            currentTexture = playTexture; //ensure the current texture is correct

            spriteBatch.Begin();
            spriteBatch.Draw(currentTexture, GameUtility.Instance.ALT_SPRITEBATCH_RECT, buttonColor);

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


        private void backButton_Click(object sender, EventArgs e)
        {
            //reverse the paused bool; unpause the game
            game.paused = !game.paused;
        }
        private void MenuGameButton_Click(object sender, EventArgs e)
        {
            /*TO-DO:
             * Call the save warning
             */
            //change the state of the game to MenuState
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            /* TO_DO:
             * Call a class that saves the current file
             * Create a message that says it has been successfully saved
             */
           
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
            /*TO-DO:
             * Call the save warning
             */
            //exit the game
            
            game.Exit();
        }
    }
}
