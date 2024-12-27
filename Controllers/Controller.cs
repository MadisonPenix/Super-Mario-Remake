using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TeamMilkGame.Controllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;

namespace TeamMilkGame
{
    public class Controller : IController
    {
        private bool hasInput;
        private IController keyboardControlls;
        private GamepadController gamepadControlls;
        private MouseController mouseControlls;
        private int playerNum;
        public IMario mario;
        public Controller(IMario mario, MilkGame game, int playerNum)
        {
            this.mario = mario;
            Type controllerScheme = Type.GetType("TeamMilkGame.KeyboardController" + (playerNum + 1));
            List<object> parameters = new List<object>();
            parameters.Add(mario);
            parameters.Add(game);
            Type[] contructorTypes = { typeof(IMario), typeof(MilkGame) };
            ConstructorInfo controllerConstructor = controllerScheme.GetConstructor(contructorTypes);
            keyboardControlls = (IController)controllerConstructor.Invoke(parameters.ToArray());
            gamepadControlls = new GamepadController(this.mario, game);
            if (playerNum == 0)
            {
                mouseControlls = new MouseController(game);
            }
            this.playerNum = playerNum;
        }
        public void Update(GameTime gameTime)
        {
            GamePadState gamepadState = GamePad.GetState(playerNum);
            hasInput = false;
            keyboardControlls.Update(gameTime);
            /*if (playerNum == 0)
            {
                mouseControlls.Update(gameTime);
            }*/
            // keyboardControlls.Update(gameTime);
            //only update gamepad if it's currently connected
            /*if (gamepadState.IsConnected)
            {
                hasInput = true;
                gamepadControlls.Update(gameTime);
            }
            else
            {
                hasInput = true;
                keyboardControlls.Update(gameTime);
            }*/
        }
        public bool HasInput()
        {
            return hasInput;
        }
        public void Vibrate()
        {
            GamePadState curState = GamePad.GetState(playerNum);
            if (curState.IsConnected)
            {
                gamepadControlls.Vibrate();
            }
        }

    }
}
