using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TeamMilkGame.Commands;
using TeamMilkGame.Controllers;

namespace TeamMilkGame
{
    //player 1 controller
    public class KeyboardController1 : IController
    {
        KeyboardState oldState;
        KeyboardState curState;
        Dictionary<Keys, ICommand> constKeyMap;
        Dictionary<Keys, ICommand> onceKeyMap;
        Dictionary<Keys, ICommand> keyUpKeyMap;
        Dictionary<Keys, ICommand> pausedKeyMap;
        ICommand IdleCommand;
        public Vector2 position { get; set; }
        private bool hasInput = false;
        private MilkGame game;

        public KeyboardController1(IMario mario, MilkGame game)
        {
            this.game = game;
            /*
             * create instances of non-static classes (downvote)
             */
            ICommand JumpCommand = new JumpCommand(mario);
            ICommand MoveLeftCommand = new MoveLeftCommand(mario);
            ICommand CrouchCommand = new CrouchCommand(mario);
            ICommand MoveRightCommand = new MoveRightCommand(mario);
            ICommand AttackCommand = new AttackCommand(mario);
            ICommand DamageCommand = new DamageCommand(mario);
            ICommand QuitCommand = new QuitCommand(game);
            ICommand ResetCommand = new ResetCommand(mario, game);
            ICommand PowerUp = new MushroomUp(mario, new Rectangle());
            ICommand FireUp = new FireUp(mario, new Rectangle());
            ICommand StarCommand = new StarUp(mario, new Rectangle());
            ICommand PauseCommand = new PauseCommand(game);
            ICommand CrouchUpCommand = new CrouchUpCommand(mario);
            ICommand SprintCommand = new SprintCommand(mario);

            IdleCommand = new IdleCommand(mario);


            /*
             * create the dictionaries
             */
            constKeyMap = new Dictionary<Keys, ICommand>();
            onceKeyMap = new Dictionary<Keys, ICommand>();
            keyUpKeyMap = new Dictionary<Keys, ICommand>();

            //keys for moving, must be able to execute constantly
            constKeyMap.Add(Keys.W, JumpCommand); //jump
            constKeyMap.Add(Keys.A, MoveLeftCommand); //left
            constKeyMap.Add(Keys.S, CrouchCommand); //crouch
            constKeyMap.Add(Keys.D, MoveRightCommand); //right
            constKeyMap.Add(Keys.LeftShift, CrouchCommand); //extra
            constKeyMap.Add(Keys.Space, JumpCommand); //extra
            //keys for attack, must only execute once per button press
            onceKeyMap.Add(Keys.Z, AttackCommand); //attack with fire ball
            constKeyMap.Add(Keys.Z, SprintCommand); //attack with fire ball
            //keys for damage, must only execute once per button press
            onceKeyMap.Add(Keys.E, DamageCommand); //mario takes damage
            //keys for using item, must only execute once per button press
            onceKeyMap.Add(Keys.D1, PowerUp);
            onceKeyMap.Add(Keys.D2, FireUp);
            onceKeyMap.Add(Keys.D3, StarCommand);
            //we want escape to open the pause menu
            onceKeyMap.Add(Keys.Escape, PauseCommand);
            //quit, must only execute once per button press
            onceKeyMap.Add(Keys.Q, QuitCommand);
            //reset, must only execute once per button press
            onceKeyMap.Add(Keys.R, ResetCommand);

            keyUpKeyMap.Add(Keys.S, CrouchUpCommand);
            keyUpKeyMap.Add(Keys.LeftShift, CrouchUpCommand);

            /*
             * add the buttons that we can use while the game is paused (prevents bugs)
             */
            pausedKeyMap = new Dictionary<Keys, ICommand>();
            pausedKeyMap.Add(Keys.Q, QuitCommand); //quit the game
            pausedKeyMap.Add(Keys.Escape, PauseCommand); //this will unpause the game
        }

        public void Update(GameTime gametime)
        {
            curState = Keyboard.GetState();
            Boolean isIdle = true;
            hasInput = false;
            //checking each key in the map to see if it's been pressed
            Keys[] pressed = curState.GetPressedKeys();


            //iterate through the pressed keys
            for (int i = 0; i < pressed.Length; i++)
            {
                //only iterate through the normal keys if the game is not paused
                if (!game.paused)
                {
                    //this is for the keys that can execute continuously
                    if (constKeyMap.ContainsKey(pressed[i]) && curState.IsKeyDown(pressed[i]))
                    {
                        constKeyMap[pressed[i]].Execute();
                        hasInput = true;
                        isIdle = false;
                    }
                    //this is for the keys that execute once per press
                    if (onceKeyMap.ContainsKey(pressed[i]) && curState.IsKeyDown(pressed[i]))
                    {
                        if (!oldState.IsKeyDown(pressed[i]))
                        {
                            onceKeyMap[pressed[i]].Execute();
                        }
                        hasInput = true;
                        isIdle = false;
                    }
                }
                else //if the game is paused...
                {
                    //iterate only through the keys in the paused menu
                    if (pausedKeyMap.ContainsKey(pressed[i]) && curState.IsKeyDown(pressed[i]))
                    {
                        if (!oldState.IsKeyDown(pressed[i]))
                        {
                            pausedKeyMap[pressed[i]].Execute();
                        }
                        hasInput = true;
                    }
                }
            }

            //this is for commands that are executed on a key up
            //for now just check for crouch up
            if (curState.IsKeyUp(Keys.S))
            {
                if (!oldState.IsKeyUp(Keys.S))
                {
                    keyUpKeyMap[Keys.S].Execute();
                }
            }
            if (curState.IsKeyUp(Keys.LeftShift))
            {
                if (!oldState.IsKeyUp(Keys.LeftShift))
                {
                    keyUpKeyMap[Keys.LeftShift].Execute();
                }
            }


            //mario stop moving!!!!!!
            if (isIdle)
            {
                IdleCommand.Execute();
            }

            //update current state
            oldState = curState;

        }
        public void Vibrate()
        {
            //last time I checked keyboards can't vibrate
        }

        public bool HasInput()
        {
            return hasInput;
        }
    }
}
