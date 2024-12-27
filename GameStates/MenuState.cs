using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class MenuState : State
    {
        private List<IComponent> primaryComponents;
        private List<IComponent> secondaryComponents;
        private Texture2D backgroundImage;
        private Texture2D titleImage;
        private Rectangle BGSource;
        private Rectangle titleSource;
        private Vector2 viewportCenter;
        private bool isGameInit;
        private int players;
        private SpriteFont buttonFont;
        //private LevelLoader levelLoader;
        private GameTransitions menuIntro;
        private float transparency;
        private Rectangle menuPosition;
        private HUD menuHUD;

        public MenuState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //sprite loading
            buttonFont = content.Load<SpriteFont>("Fonts/SmallFont");
            backgroundImage = content.Load<Texture2D>("1-1_with_blocks");
            titleImage = content.Load<Texture2D>("mario bros title screen");
            BGSource = new Rectangle(0, 0, GameUtility.Instance.BACKGROUND_WIDTH, backgroundImage.Height);
            titleSource = GameUtility.Instance.BG_RECT;

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            int textWidth = (int)buttonFont.MeasureString(GameUtility.Instance.ONE_PLAY_STR).X;

            menuIntro = new GameTransitions(game, graphicsDevice, content);
            transparency = GameUtility.Instance.INITIAL_ALPHA_VAL;
            menuPosition = new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            menuHUD = new HUD("1-1", -1); // timer on menu is not set
            this.isGameInit = false;
            this.players = 0;

            /*
             * Primary Buttons
             */
            Button onePGameButton = new Button(buttonFont)
            {
                //sets the text to be centered in the screen (horizontally)
                buttonPosition = new Vector2(viewportCenter.X - textWidth / 2f, GameUtility.Instance.ONE_PLAY_BUTTON_Y),
                buttonText = GameUtility.Instance.ONE_PLAY_STR,
            };

            onePGameButton.Click += OnePGameButton_Click;

            Button twoPGameButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X - textWidth / 2f, GameUtility.Instance.TWO_PLAY_BUTTON_Y),
                buttonText = GameUtility.Instance.TWO_PLAY_STR,
            };

            twoPGameButton.Click += TwoPGameButton_Click;

            Button quitGameButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X - textWidth / 2f, GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.QUIT_STR,
            };

            quitGameButton.Click += QuitGameButton_Click;

            primaryComponents = new List<IComponent>()
            {
                onePGameButton,
                twoPGameButton,
                quitGameButton,
            };
            /*
             * Secondary Buttons
             */
            Button levelOneButton = new Button(buttonFont)
            {
                //sets the text to be centered in the screen (horizontally)
                buttonPosition = new Vector2(viewportCenter.X + GameUtility.Instance.MENU_BUTTON_OFFSET_X,
                    GameUtility.Instance.ONE_PLAY_BUTTON_Y),
                buttonText = GameUtility.Instance.LEVEL_ONE_STR,
            };

            levelOneButton.Click += levelOneButton_Click;

            Button newLevelButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X + GameUtility.Instance.MENU_BUTTON_OFFSET_X,
                    GameUtility.Instance.TWO_PLAY_BUTTON_Y),
                buttonText = GameUtility.Instance.NEW_LEVEL_STR,
            };

            newLevelButton.Click += newLevelButton_Click;

            Button loadLevelButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X + GameUtility.Instance.MENU_BUTTON_OFFSET_X,
                    GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.LOAD_LEVEL_STR,
            };

            loadLevelButton.Click += loadLevelButton_Click;

            secondaryComponents = new List<IComponent>()
            {
                levelOneButton,
                newLevelButton,
                loadLevelButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!menuIntro.DrawMenuIntro(spriteBatch)) return; // transition 

            // draw the background
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, menuPosition, BGSource, Color.White);

            // draw the title image
            Vector2 locationPoint = new Vector2(viewportCenter.X - titleSource.Width * GameUtility.Instance.TITLE_MULT / 2f,
                GameUtility.Instance.TITLE_Y_POS);
            Vector2 sizePoint = new Vector2(titleSource.Width * GameUtility.Instance.TITLE_MULT,
                titleSource.Height * GameUtility.Instance.TITLE_MULT);

            spriteBatch.Draw(titleImage,
                //sets the text to be centered in the screen (horizontally)
                new Rectangle(locationPoint.ToPoint(), sizePoint.ToPoint()), titleSource, Color.White);
            spriteBatch.End();

            // draw the buttons
            // fade in text, if not finished yet then don't draw the buttons normally
            if (!FadeIn(gameTime, spriteBatch, transparency)) return;

            // fade in transition is finished, so draw the buttons regularly
            spriteBatch.Begin();
            foreach (IComponent component in primaryComponents)
            {
                component.DrawWithColor(gameTime, spriteBatch, Color.White);
            }
            menuHUD.Draw(spriteBatch);

            //if it's single player, draw the secondary menu to give options
            if (players == 1)
            {
                spriteBatch.DrawString(buttonFont, "<", GameUtility.Instance.ARROW_ONE_POS, Color.White);
                //draw the buttons
                foreach (IComponent component in secondaryComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }
            }
            else if (players == 2)
            {
                spriteBatch.DrawString(buttonFont, "<", GameUtility.Instance.ARROW_TWO_POS, Color.White);
                //draw the buttons
                foreach (IComponent component in secondaryComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }
            }

            spriteBatch.End();

            // incrementing background texture start position to have the menu screen "slide"
            if (gameTime.TotalGameTime.Ticks % 2 != 0) BGSource.X++;
            if (BGSource.X == GameUtility.Instance.BG_SOURCE_MAX) BGSource.X = 0; // loops the sliding so it doesn't go off the sprite image
        }
        private bool FadeIn(GameTime gameTime, SpriteBatch spriteBatch, float alpha)
        {
            // returns bool to tell Draw() if the effect has finished yet,
            // if it hasn't then it won't draw the buttons regularly
            bool temp = false;
            spriteBatch.Begin();
            /* alpha value determines transparency amount, ranges from 0 to 1.
             * Color has a constructor for both a Vector3 and Vector4 parameter,
             * Vector3 is the R,G,B values, and the Vector4 is the Vector3 * alpha */
            switch (alpha)
            {
                // max alpha value is 1.0, but 3.0 is to add a small delay after it finishes
                case >= 3.0f:
                    temp = true; // the fade-in effect is finished when alpha is >= 1.0, so return true after delay
                    break;
                case < 3.0f:
                    // increment transparency until it reaches its max, i.e. 100% opacity.
                    // changing this value changes how long the fade-in lasts
                    transparency += GameUtility.Instance.ALPHA_INCREASE_AMT;
                    foreach (IComponent component in primaryComponents)
                    {
                        component.DrawWithColor(gameTime, spriteBatch, Color.White * alpha); // Vector3 Color.White * transparency amt.
                    }
                    break;
            }
            spriteBatch.End();
            return temp;
        }

        private void OnePGameButton_Click(object sender, ButtonArgs e)
        {
            //this avoids some bugs
            game.paused = false;
            this.players = 1;

        }

        private void TwoPGameButton_Click(object sender, ButtonArgs e)
        {
            //avoid bugs
            game.paused = false;
            this.players = 2;
            //change the state to GameState
        }


        private void levelOneButton_Click(object sender, EventArgs e)
        {
            //this avoids some bugs
            game.paused = false;
            //change the state to GameState
            this.isGameInit = true;
        }
        private void newLevelButton_Click(object sender, EventArgs e)
        {
            //this avoids some bugs
            game.paused = false;
            //change the state to LevelEditor
            game.ChangeState(new LevelEditorState(game, graphicsDevice, content));
        }
        private void loadLevelButton_Click(object sender, EventArgs e)
        {
            //this avoids some bugs
            game.paused = false;
            //change the state to LevelSelectState
            game.ChangeState(new LevelSelectState(game, graphicsDevice, content));
        }

        public override void Update(GameTime gameTime)
        {
            //load level 1-1
            if (this.isGameInit)
            {
                this.isGameInit = false;
                LevelManager.Instance.LoadWorld(GameUtility.Instance.LEVEL_ONE_XML_STR, players);
                GameObjectManager.Instance.Update();
                LevelManager.Instance.Update(gameTime);
                game.ChangeState(new LevelStartState(game, graphicsDevice, content));
            }
            else
            {
                //update the buttons
                foreach (IComponent component in primaryComponents)
                {
                    component.Update(gameTime);
                }
                //update the buttons
                foreach (IComponent component in secondaryComponents)
                {
                    component.Update(gameTime);
                }
                menuIntro.Update(gameTime);
                menuHUD.Update(gameTime);
            }
        }

        private void QuitGameButton_Click(object sender, ButtonArgs e)
        {
            //exit the game
            game.Exit();
        }
    }
}
