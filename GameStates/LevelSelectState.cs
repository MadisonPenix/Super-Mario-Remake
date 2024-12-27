using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class LevelSelectState : State
    {
        private List<IComponent> components;
        private List<List<IComponent>> levelPages;
        private Texture2D backgroundImage;
        private Texture2D listBGImage;
        private Rectangle BGSource;
        private Rectangle listBGSource;
        private Vector2 viewportCenter;
        private bool isGameInit;
        private int players;
        private int level_button_y = GameUtility.Instance.LEVEL_SELECT_START_Y;
        private string selectedLevel;
        private int levelSelectPageNum;

        public LevelSelectState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //sprite loading
            SpriteFont buttonFont = content.Load<SpriteFont>("Fonts/SmallFont");
            backgroundImage = content.Load<Texture2D>("1-1_with_blocks");
            listBGImage = content.Load<Texture2D>("mario bros level list bg");
            BGSource = new Rectangle(0, 0, GameUtility.Instance.BACKGROUND_WIDTH, backgroundImage.Height);
            listBGSource = GameUtility.Instance.LEVEL_SELECT_SCREEN_RECT;
            //a vector2 that stores the center of the screen
            viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            int textWidth = (int)buttonFont.MeasureString(GameUtility.Instance.ONE_PLAY_STR).X;
            this.isGameInit = false;
            this.players = 1;

            /*
             * Buttons
             */
            Button nextPageButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(viewportCenter.X + textWidth / 2, GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET + 60),
                buttonText = GameUtility.Instance.NEXT_PAGE_STR,
            };
            nextPageButton.Click += NextPageButton_Click;

            Button prevPageButton = new Button(buttonFont)
            {
                buttonPosition = new Vector2(viewportCenter.X - textWidth, GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET + 60),
                buttonText = GameUtility.Instance.PREV_PAGE_STR,
            };
            prevPageButton.Click += PrevPageButton_Click;

            Button quitGameButton = new Button(buttonFont)
            {
                //sets the text to be lined up with the previous button
                buttonPosition = new Vector2(viewportCenter.X - textWidth / 2, GameUtility.Instance.MAIN_MENU_QUIT_BUTTON_Y_OFFSET),
                buttonText = GameUtility.Instance.QUIT_STR,
            };

            quitGameButton.Click += QuitGameButton_Click;

            components = new List<IComponent>()
            {
                nextPageButton,
                prevPageButton,
                //quitGameButton,
            };

            //Initialize list of pages of level buttons
            levelPages = new List<List<IComponent>>();
            for(int i = 0; i < (LevelManager.Instance.GetLevelXMLPaths().Length / GameUtility.Instance.LEVEL_SELECT_MAX_PER_PAGE) + 1; i++)
            {
                levelPages.Add(new List<IComponent>());
            }

            levelSelectPageNum = 0;
            //Initialize button for each level in XML levels folder
            foreach (string level in LevelManager.Instance.GetLevelXMLPaths())
            {
                //If max levels have been reached on page, move to next page
                int index = Array.IndexOf(LevelManager.Instance.GetLevelXMLPaths(), level);
                if (index % GameUtility.Instance.LEVEL_SELECT_MAX_PER_PAGE == 0 && index != 0) 
                { 
                    levelSelectPageNum++;
                    level_button_y = GameUtility.Instance.LEVEL_SELECT_START_Y;
                }
 
                string levelName = level.Substring(0, level.Length - 4).Replace(LevelManager.Instance.GetDirectory(), "");
                textWidth = (int)buttonFont.MeasureString(levelName).X;
                Button levelButton = new Button(buttonFont)
                {
                    //sets the text to be lined up with the previous button
                    buttonPosition = new Vector2(viewportCenter.X - textWidth / 2, level_button_y),
                    buttonText = levelName,
                };

                levelButton.Click += LevelButton_Click;
                levelPages[levelSelectPageNum].Add(levelButton);
                level_button_y += GameUtility.Instance.LEVEL_SELECT_BUTTON_SPACING;
                
            }
            //Reset page index to 0
            levelSelectPageNum = 0;
        }

        private void LevelButton_Click(object sender, ButtonArgs e)
        {
            selectedLevel = e.levelXMLPath;
            //this avoids some bugs
            game.paused = false;
            //change the state to GameState
            this.isGameInit = true;
        }

        private void NextPageButton_Click(object sender, ButtonArgs e)
        {
            if(levelSelectPageNum < levelPages.Count - 1)
            {
                levelSelectPageNum++;
            }
        }
        private void PrevPageButton_Click(object sender, ButtonArgs e)
        {
            if(levelSelectPageNum > 0)
            {
                levelSelectPageNum--;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            //draw the background
            spriteBatch.Draw(
                backgroundImage,
                new Rectangle(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height),
                BGSource, Color.White
            );
            //draw the title image
            Vector2 locationPoint = new Vector2(viewportCenter.X - listBGSource.Width * GameUtility.Instance.TITLE_MULT / 2, GameUtility.Instance.TITLE_Y_POS + GameUtility.Instance.MENU_BUTTON_Y_OFFSET);
            Vector2 sizePoint = new Vector2(listBGSource.Width * GameUtility.Instance.TITLE_MULT, listBGSource.Height * GameUtility.Instance.TITLE_MULT);
            spriteBatch.Draw(listBGImage,
                //sets the text to be centered in the screen (horizontally)
                new Rectangle(locationPoint.ToPoint(), sizePoint.ToPoint()), listBGSource, Color.White);

            //draw the buttons that are on every page
            foreach (IComponent component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            //draw the level select buttons
            foreach (IComponent page in levelPages[levelSelectPageNum])
            {
                page.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            // incrementing background texture start position to have the menu screen "slide"
            if (gameTime.TotalGameTime.Ticks % 2 != 0) BGSource.X++;
            if (BGSource.X == GameUtility.Instance.BG_SOURCE_MAX) BGSource.X = 0; // loops the sliding so it doesn't go off the sprite image

        }
        public override void Update(GameTime gameTime)
        {
            if (this.isGameInit)
            {
                LevelManager.Instance.LoadWorld(selectedLevel, players);
                GameObjectManager.Instance.Update();
                LevelManager.Instance.Update(gameTime);
                this.isGameInit = false;
                game.ChangeState(new LevelStartState(game, graphicsDevice, content));
            }
            else
            {
                //draw the buttons that are on every page
                foreach (IComponent component in components)
                {
                    component.Update(gameTime);
                }

                //update the buttons
                foreach (IComponent component in levelPages[levelSelectPageNum])
                {
                    component.Update(gameTime);
                }
            }
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            //exit the game
            game.Exit();
        }
    }
}
