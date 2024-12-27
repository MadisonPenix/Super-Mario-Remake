using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class GOverState : State
    {
        private List<IComponent> components;
        private Vector2 viewportCenter;
        private SpriteFont textFont;
        private SpriteFont buttonFont;

        public GOverState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //sprite loading
            textFont = content.Load<SpriteFont>("Fonts/Font");
            buttonFont = content.Load<SpriteFont>("Fonts/SmallFont");

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            int textHeight = (int)buttonFont.MeasureString(GameUtility.Instance.QUIT_STR).Y;

            /*
             * Buttons
             */
            Button continueButton = new Button(buttonFont)
            {
                //sets the text to be centered in the screen (horizontally)
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.CONTINUE_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.CONT_Y_OFFSET),
                buttonText = GameUtility.Instance.CONTINUE_STR,
            };

            continueButton.Click += continueButton_Click;

            Button quitGameButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.QUIT_STR).X / 2, GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.QUIT_STR,
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<IComponent>()
            {
                continueButton,
                quitGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //draw a black background
            graphicsDevice.Clear(Color.Black);
            //draw game over text
            spriteBatch.DrawString(textFont, GameUtility.Instance.GAME_OVER_STR,
                    viewportCenter - textFont.MeasureString(GameUtility.Instance.GAME_OVER_STR) / 2, Color.White);

            //draw the buttons
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }
        private void continueButton_Click(object sender, EventArgs e)
        {
            /*TO-DO:
             * Check what the previous state was, and change to that state
             */
            //change the state to GameState
            game.ChangeState(new LevelStartState(game, graphicsDevice, content));
        }

        public override void Update(GameTime gameTime)
        {

            //update the buttons
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            //exit to the menu
            game.ChangeState(new MenuState(game, graphicsDevice, content));
        }
    }
}
