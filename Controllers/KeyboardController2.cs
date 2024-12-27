using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TeamMilkGame.Commands;
using TeamMilkGame.Controllers;
using System.Diagnostics;

namespace TeamMilkGame
{
    //player 2 controller
    public class KeyboardController2 : IController
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

        public KeyboardController2(IMario mario, MilkGame game)
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

            //arrow keys. Eventually used for Luigi?
            constKeyMap.Add(Keys.Up, JumpCommand); //jump
            constKeyMap.Add(Keys.Left, MoveLeftCommand); //left
            constKeyMap.Add(Keys.Down, CrouchCommand); //crouch
            constKeyMap.Add(Keys.Right, MoveRightCommand); //right
            //keys for attack, must only execute once per button press
            onceKeyMap.Add(Keys.N, AttackCommand); //attack with fire ball
            constKeyMap.Add(Keys.N, SprintCommand); //attack with fire ball
            //keys for damage, must only execute once per button press
            onceKeyMap.Add(Keys.L, DamageCommand); //mario takes damage
            //keys for using item, must only execute once per button press
            onceKeyMap.Add(Keys.D5, PowerUp);
            onceKeyMap.Add(Keys.D6, FireUp);
            onceKeyMap.Add(Keys.D7, StarCommand);

            //reset, must only execute once per button press
            onceKeyMap.Add(Keys.R, ResetCommand);

            keyUpKeyMap.Add(Keys.Down, CrouchUpCommand);
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
            }

            if (curState.IsKeyUp(Keys.Down))
            {
                if (!oldState.IsKeyUp(Keys.Down))
                {
                    keyUpKeyMap[Keys.Down].Execute();
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
