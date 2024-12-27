using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TeamMilkGame.Collision;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
//contemplate creating a button in the top left corner of the screen to pause and unpause the game
namespace TeamMilkGame.GameStates
{
    public class GameState : State
    {
        /*
         * game variables
         */
        public Camera camera { get; set; }
        private LevelLoader levelLoader;

        /*
         * paused menu variables
         */
        private bool paused;
        private SpriteFont buttonFont;
        private Texture2D pauseTexture;
        private State PauseState;
        private State GOverState;
        private double timer;
        private double updateDeathTimer;
        List<IComponent> components;
        IMario mario;
        //needs to be able to be used by other methods
        private Button pauseGameButton;
        private HUD gameHUD;

        public GameState(MilkGame game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            timer = GameUtility.Instance.LEVEL_DELAY_TIMER;
            updateDeathTimer = 0.5;
            PauseState = new PauseState(game, graphicsDevice, content);
            GOverState = new GOverState(game, graphicsDevice, content);

            //gameHUD = GameObjectManager.Instance.Drawable.OfType<Background>().Single().levelHUD;



            //the code that was in MilkGame.Initialize

            this.camera = game.camera;

            //pause button sprites
            buttonFont = content.Load<SpriteFont>("Fonts/Font");
            pauseTexture = content.Load<Texture2D>("pauseButton");

            int textHeight = (int)buttonFont.MeasureString(GameUtility.Instance.MENU_STR).Y;


            //the button in the top left corner of the screen to pause and unpause
            pauseGameButton = new Button(buttonFont)
            {
                buttonPosition = GameUtility.Instance.PAUSE_POS,
                buttonText = "  ", //no text, but needs a bounding box
            };
            pauseGameButton.Click += PauseGameButton_Click;

            pauseGameButton = new Button(buttonFont)
            {
                buttonPosition = GameUtility.Instance.PAUSE_POS,
                buttonText = "  ", //no text, but needs a bounding box
            };
            pauseGameButton.Click += PauseGameButton_Click;

            components = new List<IComponent>()
            {
                pauseGameButton,
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //get the center of the screen
            Vector2 viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2f, graphicsDevice.Viewport.Height / 2f);
            graphicsDevice.Clear(LevelManager.Instance.backgroundColor);

            //set a transluscency color for the pause/play buttons
            Color buttonColor = GameUtility.Instance.BUTTON_COLOR;

            //draw the level
            Vector2 cameraParallax = new Vector2(1.0f); // basically determines how much the camera actually moves with Mario
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TransformViewMatrix(cameraParallax)); // all of this going into spriteBatch.Begin() controls the camera
            if (LevelManager.Instance.isLevelLoadedCurrently)
            {
                foreach (IGameObject gameObj in GameObjectManager.Instance.Drawable)
                {
                    gameObj.Draw(spriteBatch);
                }
            }
            LevelManager.Instance.playHUD.Update(gameTime);
            spriteBatch.End();

            

            //if the game is paused, draw the paused overlay
            if (paused)
            {
                PauseState.Draw(gameTime, spriteBatch);
            }
            else
            {
                //it is unfortunate to call spriteBatch for one image
                spriteBatch.Begin(); //spritebatch twice so that the camera doesn't affect the text location
                spriteBatch.Draw(pauseTexture, GameUtility.Instance.ALT_SPRITEBATCH_RECT, buttonColor);
                LevelManager.Instance.playHUD.Draw(spriteBatch);
                spriteBatch.End();
            }
        }
        private void PauseGameButton_Click(object sender, EventArgs e)
        {
            
                game.paused = true;

        }

        public override void Update(GameTime gameTime)
        {
            if (LevelManager.Instance.isLevelLoadedCurrently)
            {

                //check for a lost life
                bool lifeLost = false;
                IMario lifeLostPlayer = (IMario)GameObjectManager.Instance.Players[0];
                foreach(IMario player in GameObjectManager.Instance.Players)
                {
                    if (player.lives.LifeLost())
                    {
                        lifeLostPlayer = player;
                        lifeLost = true;
                    }
                }
                if (lifeLost)
                {
                    //wait a bit before respawning
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;
                    updateDeathTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (updateDeathTimer <= 0)
                    {
                        lifeLostPlayer.Update(gameTime);
                    }
                    if (timer <= 0)
                    {

                        //reset mario and the camera
                        lifeLostPlayer.lives.Respawn();
                        camera.ResetCamera(GameUtility.Instance.INIT_CAMERA_POS);
                        //reset the timer for next time
                        timer = GameUtility.Instance.LEVEL_DELAY_TIMER;
                        updateDeathTimer = 0.5;
                        //check for a game over state
                        if (lifeLostPlayer.lives.GameOver())
                        {
                            game.ChangeState(GOverState);
                        }
                        else
                        {
                            game.ChangeState(new LevelStartState(game, graphicsDevice, content));
                        }
                    }
                }
                //only update game objects if the game is not paused
                else if (!paused)
                {
                    LevelManager.Instance.playHUD.Update(gameTime);
                    CheckMario();

                    foreach (IGameObject gameObj in GameObjectManager.Instance.Updatable)
                    {
                        gameObj.Update(gameTime);
                    }
                    camera.Update();
                    //gameHUD.Update(gameTime);


                    //we need to allow the pause button to update when the game isn't paused
                    pauseGameButton.Update(gameTime);
                    CollisionDetection.Instance.Detect();

                }
                //if the game is paused, call PauseState
                else
                {
                    PauseState.Update(gameTime);
                }
            }
            GameObjectManager.Instance.Update();
            //update paused
            this.paused = game.paused;
        }
        public void Reset()
        {
            mario.Reset();
            camera.ResetCamera(CameraUtility.Instance.X_OFFSET);
            //GameObjectManager.Instance.Clear();
        }

        //NEED TO ADD CHECK IF MARIO IS DEAD TOO
        private void CheckMario()
        {
            foreach (IMario Mario in GameObjectManager.Instance.Players)
            {
                if (Mario.IsFlagPole)
                {
                    game.ChangeState(new LevelClearState(game, Mario, graphicsDevice, content));
                }
                //kill box below level

                if (Mario.Position.Y > MarioUtility.Instance.KILL_BOX_Y_POS)
                {
                    //should cause mario to keep taking damage until he dies but needs to be tested
                    Mario.TakeDamage();
                }
                
            }
        }
    }
}
