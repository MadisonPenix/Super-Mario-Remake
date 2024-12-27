using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class Button : IComponent
    {
        //font variables
        private SpriteFont font;
        private int fontHeight;
        private int fontWidth;
        private bool isHovering;
        //mouse variables
        private MouseState currentMouse;
        private MouseState previousMouse;
        //button variables
        public event EventHandler<ButtonArgs> Click;
        public bool Clicked { get; private set; }
        public Color PenColor { get; set; }
        public Vector2 buttonPosition { get; set; }
        public string buttonText { get; set; }
        public Button(SpriteFont Font)
        {
            font = Font;
            PenColor = Color.White; //set the color of the text to white
        }
        public Rectangle rectangle
        {
            //create a rectangle for the bounding box of the button
            get
            {
                return new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, (int)font.MeasureString(buttonText).X, (int)font.MeasureString(buttonText).Y);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            PenColor = Color.White;
            //change the text color if the cursor is hovering overtop
            if (isHovering)
            {
                PenColor = Color.Gray;
            }
            //check if buttonText has text before drawing it
            if (!string.IsNullOrEmpty(buttonText))
            {
                int x = (int)buttonPosition.X;
                int y = (int)buttonPosition.Y;
                spriteBatch.DrawString(font, buttonText, new Vector2(x, y), PenColor);
            }
        }

        public override void DrawWithColor(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            //check if buttonText has text before drawing it
            if (!string.IsNullOrEmpty(buttonText))
            {
                int x = (int)buttonPosition.X;
                int y = (int)buttonPosition.Y;
                spriteBatch.DrawString(font, buttonText, new Vector2(x, y), color);
            }
        }

        public override void Update(GameTime gameTime)
        {
            //update the mouse
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, GameUtility.Instance.BUTTON_DIM, GameUtility.Instance.BUTTON_DIM);

            isHovering = false;

            //check if the cursor is in the rectangles of the buttons
            if (mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;

                //check if the mouse is getting clicked
                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    ButtonArgs args = new ButtonArgs();
                    args.levelXMLPath = this.buttonText + ".xml";
                    Click?.Invoke(this, args);
                }
            }
        }
    }

    //Level button pressing event arguments
    public class ButtonArgs : EventArgs
    {
        public string levelXMLPath { get; set; }
        public IGameObject gameObject { get; set; }
    }
}
