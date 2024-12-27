using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TeamMilkGame.Commands;
using TeamMilkGame.Controllers;


namespace TeamMilkGame
{
    public class GamepadController : IController
    {
        private GamePadState oldState;
        private GamePadState curState;
        private Dictionary<Buttons, ICommand> constKeyMap;
        private Dictionary<Buttons, ICommand> onceKeyMap;
        private ICommand IdleCommand;
        private float VibrationAmount = 0.3f;
        private bool vibrate;
        private bool disable;
        private bool hasInput;

        //seconds for vibration
        private double VibrateTime = 3;

        public GamepadController(IMario mario, MilkGame game)
        {

            /*
             * create instances of non-static classes (downvote)
             */
            ICommand JumpCommand = new JumpCommand(mario);
            ICommand MoveLeftCommand = new MoveLeftCommand(mario);
            ICommand CrouchCommand = new CrouchCommand(mario);
            ICommand MoveRightCommand = new MoveRightCommand(mario);
            ICommand AttackCommand = new AttackCommand(mario);
            ICommand QuitCommand = new QuitCommand(game);
            ICommand ResetCommand = new ResetCommand(mario, game);
            ICommand PauseCommand = new PauseCommand(game);
            ICommand DamageCommand = new DamageCommand(mario, this);
            IdleCommand = new IdleCommand(mario);

            /*
             * create the dictionaries
             */
            constKeyMap = new Dictionary<Buttons, ICommand>();
            onceKeyMap = new Dictionary<Buttons, ICommand>();
            //keys for moving, must be able to execute constantly
            constKeyMap.Add(Buttons.A, JumpCommand); //jump
            constKeyMap.Add(Buttons.DPadLeft, MoveLeftCommand); //left
            constKeyMap.Add(Buttons.DPadDown, CrouchCommand); //crouch
            constKeyMap.Add(Buttons.DPadRight, MoveRightCommand); //right
            //keys for attack, must only execute once per button press
            onceKeyMap.Add(Buttons.B, AttackCommand); //attack with fire ball
            //open and close the pause menu
            onceKeyMap.Add(Buttons.Start, PauseCommand);
            onceKeyMap.Add(Buttons.Y, DamageCommand);
            //quit, must only execute once per button press
            onceKeyMap.Add(Buttons.Back, QuitCommand);
            //reset, must only execute once per button press
            onceKeyMap.Add(Buttons.X, ResetCommand);

            this.vibrate = true;
            this.disable = false;
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
        }
        public void Update(GameTime gameTime)
        {
            GamePadState curState = GamePad.GetState(PlayerIndex.One);
            Boolean isIdle = true;
            //check if the controller is connected
            if (curState.IsConnected)
            {
                //this is for the keys that can execute continuously
                foreach (var button in constKeyMap)
                {
                    if (curState.IsButtonDown(button.Key))
                    {
                        constKeyMap[button.Key].Execute();
                        isIdle = false;
                    }
                }
                //this is for the keys that execute once per press
                foreach (var button in onceKeyMap)
                {
                    if (curState.IsButtonDown(button.Key))
                    {
                        if (!oldState.IsButtonDown(button.Key))
                        {
                            onceKeyMap[button.Key].Execute();
                        }
                        isIdle = false;
                    }
                }
            }

            //mario stop moving!!!!!!
            if (isIdle)
            {
                IdleCommand.Execute();
            }

            //update current state
            oldState = curState;

            //update the vibration time
            if (!disable)
            {
                if (vibrate)
                {
                    GamePad.SetVibration(PlayerIndex.One, VibrationAmount, VibrationAmount);
                    vibrate = false;
                }
                if (VibrateTime > 0)
                {
                    VibrateTime -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    // Stop the vibration
                    GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                    disable = true;
                }
            }
        }
        public void Vibrate()
        {
            GamePad.SetVibration(PlayerIndex.One, VibrationAmount, VibrationAmount);
        }
        public Boolean HasInput()
        {
            return hasInput;
        }
    }
}
