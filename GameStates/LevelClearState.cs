using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using TeamMilkGame.Collision;
using TeamMilkGame.Commands;
using TeamMilkGame.States;
using TeamMilkGame.UtilityClasses;
//contemplate creating a button in the top left corner of the screen to pause and unpause the game
namespace TeamMilkGame.GameStates
{
    public class LevelClearState : State
    {
        /*
         * game variables
         */
        public Camera camera { get; set; }
        private LevelLoader levelLoader;
        private Controller controlls;
        //needs to be able to be used by other methods
        private Button pauseGameButton;
        private double timer;
        private IMario player;
        private TeamMilkGame.Commands.ICommand moveRightCommand;
        private TeamMilkGame.Commands.ICommand jumpCommand;
        public LevelClearState(MilkGame game, IMario player, GraphicsDevice graphicsDevice, ContentManager content)
            : base(game, graphicsDevice, content)
        {
            //not letting the user pause during this state because that could be potentially problematic

            /*TO-DO
             * Make mario slide down the flagpole
             * Make mario walk into the castle
             * Raise the flag on the castle
             * Call the main menu
             */
            // Debug.WriteLine("Level clear state entered");

            //Set up player and commands to use in flagpole animation
            this.player = player;
            moveRightCommand = new MoveRightCommand(this.player);
            jumpCommand = new JumpCommand(this.player);

            timer = GameUtility.Instance.LEVEL_END_TIMER; //for now setting this state on a timer and then switching to the main menu

            //the code that was in MilkGame.Initialize
            camera = new Camera(graphicsDevice.Viewport);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //get the center of the screen
            Vector2 viewportCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            graphicsDevice.Clear(Color.CornflowerBlue);

            //draw the level
            Vector2 cameraParallax = new Vector2(1.0f); // basically determines how much the camera actually moves with Mario
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, camera.TransformViewMatrix(cameraParallax)); // all of this going into spriteBatch.Begin() controls the camera

            foreach (var GameObj in GameObjectManager.Instance.Drawable)
            {
                GameObj.Draw(spriteBatch);
            }
            spriteBatch.End();


        }

        public override void Update(GameTime gameTime)
        {
            DescendFlagpoleAnimation();

            GameObjectManager.Instance.Update();
            foreach (var GameObj in GameObjectManager.Instance.Updatable)
            {
                GameObj.Update(gameTime);
            }

            camera.Update();

            CollisionDetection.Instance.Detect();

            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            //switch to the menu
            if (timer <= 0)
            {
                game.ChangeState(new MenuState(game, graphicsDevice, content));
            }
        }

        //CLEAR OUT MAGIC NUMBERS
        private void DescendFlagpoleAnimation()
        {
            if (this.player.Position.Y < (1000 - 64 * 4.5))
            {
                this.player.physics.SetVelocity(new Vector2(0, 0));
                this.player.Position += new Vector2(0, 5);
            }
            else if ((this.player.IsFlagPole))
            {
                this.player.IsFlagPole = false;
                moveRightCommand.Execute();
                jumpCommand.Execute();
            }
            else if (this.player.Position.X < 12990)
            {
                moveRightCommand.Execute();
            }
        }
    }
}
