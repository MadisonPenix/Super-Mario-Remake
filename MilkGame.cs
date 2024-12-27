using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TeamMilkGame.GameStates;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
using System;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using System.Diagnostics;

namespace TeamMilkGame
{
    public class MilkGame : Game
    {
        private GraphicsDeviceManager graphics;
        private LevelLoader levelLoader;
        private SpriteBatch spriteBatch;
        private static List<Controller> Controllers;
        public bool paused = false; //start the game paused
        public static SpriteFont gameFont;

        public Camera camera { get; set; }

        //game states
        private State currentState { get; set; }
        private State nextState;
        private HUD hudDisplay;

        public void ChangeState(State state)
        {
            nextState = state;
        }
        public MilkGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameUtility.Instance.GRAPHICS_BUFFER_WIDTH;
            graphics.PreferredBackBufferHeight = GameUtility.Instance.GRAPHICS_BUFFER_HEIGHT;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Texture2D MouseTexture = Content.Load<Texture2D>("cursor");
            Mouse.SetCursor(MouseCursor.FromTexture2D(MouseTexture, 0, 0));
        }


        protected override void Initialize()
        {
            Controllers = new List<Controller>();
            camera = new Camera(GraphicsDevice.Viewport);
            LevelManager.Instance.Initialize(this, camera, Controllers, Content);// Camera takes in viewport from main game class

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Texture2DStorage.LoadAllTextures(Content);
            SpriteFactory.Instance.LoadAllTextures();
            SoundFXStorage.Instance.LoadAllSounds(Content);

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            gameFont = Content.Load<SpriteFont>("Fonts/SmallFont");
            currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            // hud on menu displays 1-1 and has no timer
            // SoundFXStorage.Instance.PlaySong("overworldTheme");
            // controlls = new Controller((IMario)GameObjectManager.Instance.Players[0], this);

        }

        protected override void Update(GameTime gameTime)
        {
            UpdateControllers(gameTime);
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            base.Update(gameTime);
            currentState.Update(gameTime);
            // hudDisplay.Update(gameTime);
            //currentState.PostUpdate(gameTime);
            LevelManager.Instance.Update(gameTime);
        }

        private void UpdateControllers(GameTime gameTime)
        {
            foreach (Controller Control in Controllers)
            {
                if (!Control.mario.IsFlagPole)
                {
                    Control.Update(gameTime);
                }
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(LevelManager.Instance.backgroundColor);
            currentState.Draw(gameTime, spriteBatch);
            // hudDisplay.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}