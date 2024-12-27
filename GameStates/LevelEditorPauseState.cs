using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
using TeamMilkGame.XML;
//contemplate creating a button in the top left corner of the screen to pause and unpause the game
namespace TeamMilkGame.GameStates
{
    public class LevelEditorPauseState : State
    {
        /*
         * paused menu variables
         */
        private bool paused;
        private bool saveWarning;
        private bool quitMode;
        private bool menuMode;
        private Controller controlls;
        private SpriteFont buttonFont;
        private Texture2D playTexture;
        private List<IComponent> components;
        private List<IComponent> saveComponents;
        private Button submitNameButton;
        private Vector2 viewportCenter;
        //needs to be able to be used by other methods
        private Button pauseGameButton;
        private String enteredText;
        private bool saveNameTime;
        private Texture2D rectangle;
        private int textHeight;
        private bool keyPressed;
        public LevelEditorPauseState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            //pause menu sprites
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            playTexture = content.Load<Texture2D>("playButton");

            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            //viewportCenter = new Vector2(camera.Viewport.Width / 2f, camera.Viewport.Height / 2f);
            textHeight = (int)buttonFont.MeasureString(GameUtility.Instance.MENU_STR).Y;
            saveNameTime = false;
            //why is making color rectangles so hard lmao
            rectangle = new Texture2D(graphicsDevice, 1, 1);
            rectangle.SetData(new[] { Color.White });
            enteredText = "";

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
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.MENU_STR,
            };

            menuGameButton.Click += MenuGameButton_Click;

            Button quitGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.QUIT_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.BUTTON_Y_OFFSET * 2),
                buttonText = GameUtility.Instance.QUIT_STR,
            };

            quitGameButton.Click += QuitGameButton_Click;

            Button backButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.BACK_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.BUTTON_Y_OFFSET * 3),
                buttonText = GameUtility.Instance.BACK_STR,
            };

            backButton.Click += backButton_Click;

            //the button in the top left corner of the screen to pause and unpause
            pauseGameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(2, 2),
                buttonText = "  ", //no text, but needs a bounding box
            };
            pauseGameButton.Click += PauseGameButton_Click;

            /*
             * Buttons for the save warning
             */
            Button saveYesButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.SAVE_STR).X / 2,
                    viewportCenter.Y - textHeight / 2),
                buttonText = GameUtility.Instance.SAVE_STR,
            };

            saveYesButton.Click += saveYesButton_Click;

            Button saveNoButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.LEAVE_STR).X / 2,
                    viewportCenter.Y - textHeight / 2 + GameUtility.Instance.BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.LEAVE_STR,
            };

            saveNoButton.Click += saveNoButton_Click;


            components = new List<IComponent>()
            {
                saveButton,
                menuGameButton,
                quitGameButton,
                backButton,
                pauseGameButton,
            };

            //save warning buttons
            saveComponents = new List<IComponent>()
            {
                saveYesButton,
                saveNoButton,
            };
            /*
             * Button for letting the user submit their file name (lots of buttons!)
             */
            submitNameButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(
                    viewportCenter.X - buttonFont.MeasureString(GameUtility.Instance.LEAVE_STR).X / 2,
                    viewportCenter.Y + textHeight + GameUtility.Instance.BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.SUBMIT_STR,
            };

            submitNameButton.Click += submitNameButton_Click;
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //get the center of the screen
            Vector2 viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);

            //set a transluscency color for the pause/play buttons
            Color buttonColor = GameUtility.Instance.BUTTON_COLOR;

            spriteBatch.Begin();
            spriteBatch.Draw(playTexture, GameUtility.Instance.ALT_SPRITEBATCH_RECT, buttonColor);

            //check which menu we are currently on
            if (!saveWarning)
            { //we are on the regular pause menu
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

            }
            else //we are on the save warning menu
            {
                //draw the save warning title
                spriteBatch.DrawString(buttonFont, GameUtility.Instance.QUIT_WO_SAVING_STR,
                    viewportCenter - buttonFont.MeasureString(GameUtility.Instance.QUIT_WO_SAVING_STR) / 2 - GameUtility.Instance.PAUSE_TEXT_OFFSET,
                    Color.White);

                //draw the buttons
                foreach (var component in saveComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }
            }
            if (saveNameTime)
            {
                
                //draw an overlay rectangle
                spriteBatch.Draw(rectangle, new Rectangle((int)viewportCenter.X - GameUtility.Instance.BIG_RECT_WIDTH/2, (int)viewportCenter.Y - GameUtility.Instance.BIG_RECT_HEIGHT/2, GameUtility.Instance.BIG_RECT_WIDTH, GameUtility.Instance.BIG_RECT_HEIGHT),
            Color.DarkGray);
                //draw rectangle for input text
                spriteBatch.Draw(rectangle, new Rectangle((int)viewportCenter.X - GameUtility.Instance.SMALL_RECT_WIDTH/2, (int)viewportCenter.Y + GameUtility.Instance.SMALL_RECT_HEIGHT/2, GameUtility.Instance.SMALL_RECT_WIDTH, textHeight),
            Color.Gray);
                //draw the user prompt
                spriteBatch.DrawString(buttonFont, "Enter save name: ", new Vector2((int)viewportCenter.X - GameUtility.Instance.TOP_TEXT_OFFSET_X, (int)viewportCenter.Y - GameUtility.Instance.TOP_TEXT_OFFSET_Y), Color.White);
                //draw the user input
                spriteBatch.DrawString(buttonFont, enteredText, new Vector2((int)viewportCenter.X - GameUtility.Instance.TOP_TEXT_OFFSET_X, (int)viewportCenter.Y + GameUtility.Instance.SMALL_RECT_HEIGHT / 2), Color.White);
                submitNameButton.Draw(gameTime, spriteBatch);
            }
            if (saveNameTime)
            {
                //draw an overlay rectangle
                spriteBatch.Draw(rectangle, new Rectangle((int)viewportCenter.X - GameUtility.Instance.BIG_RECT_OFFSET_X, (int)viewportCenter.Y - GameUtility.Instance.BIG_RECT_OFFSET_Y, GameUtility.Instance.BIG_RECT_OFFSET_X, GameUtility.Instance.BIG_RECT_OFFSET_Y),
            Color.DarkGray);
                //draw rectangle for input text
                spriteBatch.Draw(rectangle, new Rectangle((int)viewportCenter.X - GameUtility.Instance.SMALL_RECT_OFFSET_X, (int)viewportCenter.Y + GameUtility.Instance.SMALL_RECT_OFFSET_Y, GameUtility.Instance.SMALL_RECT_OFFSET_X, textHeight),
            Color.Gray);
                //draw the user prompt
                spriteBatch.DrawString(buttonFont, "Enter save name: ", new Vector2((int)viewportCenter.X - GameUtility.Instance.TOP_TEXT_OFFSET_X, (int)viewportCenter.Y - GameUtility.Instance.TOP_TEXT_OFFSET_Y), Color.White);
                //draw the user input
                spriteBatch.DrawString(buttonFont, enteredText, new Vector2((int)viewportCenter.X - GameUtility.Instance.TOP_TEXT_OFFSET_X, (int)viewportCenter.Y + GameUtility.Instance.SMALL_RECT_OFFSET_Y), Color.White);
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
            //call the save warning
            saveWarning = true;
            menuMode = true;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            //brings up the text box to let the user put in a file name
            //saveNameTime = true;
            /*
             * TO-DO: 
             * Create a message letting the user know the game has been successfully saved
             */
            saveNameTime = true;
            
        }
        private void PauseGameButton_Click(object sender, EventArgs e)
        {
            game.paused = false;
        }

        public override void Update(GameTime gameTime)
        {
            //check which menu we are currently on
            if (!saveWarning)
            { //we are on the regular pause menu
                foreach (var component in components)
                {
                    component.Update(gameTime);
                }
            }
            else //we are on the save warning menu
            {
                foreach (var component in saveComponents)
                {
                    component.Update(gameTime);
                }
            }
            //get the user input for the XML file name
            if (saveNameTime)
            {
                getSaveName();
                submitNameButton.Update(gameTime);
            }
            //update paused
            this.paused = game.paused;
        }
        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            //call the save warning
            quitMode = true;
            saveWarning = true;
        }
        private void saveYesButton_Click(object sender, EventArgs e)
        {
            //no longer need the save warning
            saveWarning = false;
            saveNameTime = true;
        }
        private void saveNoButton_Click(object sender, EventArgs e)
        {
            //no longer need the save warning
            saveWarning = false;
            //check if the user had presssed quit or menu
            if (quitMode) //the user selected quit
            {
                quitMode = false; //reset for next time
                //exit the game
                game.Exit();
            }
            else //the user selected menu
            {
                //change the state of the game to MenuState
                game.ChangeState(new MenuState(game, graphicsDevice, content));
            }
        }

        /**
        private void getSaveName()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();

            foreach (Keys key in pressedKeys)
            {
                if (!keyPressed)
                {
                    //let the user backspace
                    if (key == Keys.Back)
                    {
                        if (key == Keys.Back && enteredText.Length > 0)
                        {
                            enteredText = enteredText.Substring(0, enteredText.Length - 1);
                            Debug.WriteLine(enteredText.Length);
                        }
                    }
                    //add the current pressed key to the string
                    else
                    {
                        //if you press a special character you die btw
                        enteredText += (char)key;
                    }
                    keyPressed = true;
                }
            }
            if (pressedKeys.Length == 0)
            {
                keyPressed = false;
            }
        }
        **/
        private void submitNameButton_Click(object sender, EventArgs e)
        {
            saveNameTime = false;

            XMLLevelEditor.Instance.writeToFile(getSaveName());

            //check if the user had presssed quit or menu
            if (quitMode) //the user selected quit
            {
                quitMode = false; //reset for next time
                //exit the game
                game.Exit();
            }
            else if (menuMode) //the user selected menu
            {
                menuMode = false;
                //change the state of the game to MenuState
                game.ChangeState(new MenuState(game, graphicsDevice, content));
            }
        }

        /*
         * Let's the user input a string. Does not handle some special characters 
         * (doesn't do anything that requires shift, including uppercase)
         */
        private String getSaveName()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] pressedKeys = keyboardState.GetPressedKeys();
            Debug.WriteLine(pressedKeys.ToString());
            foreach (Keys key in pressedKeys)
            {
                //let the user backspace
                if (key == Keys.Back && enteredText.Length > 0)
                {
                    enteredText = enteredText.Substring(0, enteredText.Length - 1);
                }
                //add the current pressed key to the string
                else
                {
                    //make sure key is not null
                    if ((char)key != '\0')
                    {
                        enteredText += (char)key;
                    }
                }
            }

            return enteredText;
            
        }
    }
}
