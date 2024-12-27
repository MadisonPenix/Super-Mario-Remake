using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.GameStates
{
    public class SpriteButton : IComponent
    {
        GraphicsDevice graphics;
        //mouse variables
        private MouseState currentMouse;
        private MouseState previousMouse;
        //button variables
        private bool isHovering;
        private ISprite buttonSprite;
        public IGameObject createdObject { get; set; }
        private Texture2D backgroundTexture;
        public event EventHandler<ButtonArgs> Click;
        public bool Clicked { get; private set; }
        public Vector2 buttonPosition { get; set; }

        public SpriteButton(ISprite sprite, Texture2D backgroundTexture)
        {
            buttonSprite = sprite;
            this.backgroundTexture = backgroundTexture;
        }

        public Rectangle rectangle
        {
            //create a rectangle for the bounding box of the button
            get
            {
                return new Rectangle((int)buttonPosition.X, (int)buttonPosition.Y, GameUtility.Instance.SPRITEBUTTON_SIDE_OFFSET + buttonSprite.spriteWidth, GameUtility.Instance.SPRITEBUTTON_SIDE_OFFSET + buttonSprite.spriteHeight);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw button background
            if (isHovering)
            {
                spriteBatch.Draw(backgroundTexture, rectangle, Color.Purple);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, rectangle, Color.Chocolate);
            }

            //Draw sprite on background
            this.buttonSprite.Draw(spriteBatch, new Vector2(rectangle.X + GameUtility.Instance.SPRITEBUTTON_SIDE_LENGTH / 8, rectangle.Y + GameUtility.Instance.SPRITEBUTTON_SIDE_LENGTH / 8));
        }

        public override void DrawWithColor(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            //Draw button background
            if (isHovering)
            {
                spriteBatch.Draw(backgroundTexture, rectangle, Color.Purple);
            }
            else
            {
                spriteBatch.Draw(backgroundTexture, rectangle, color);
            }

            //Draw sprite on background
            this.buttonSprite.Draw(spriteBatch, new Vector2(rectangle.X + GameUtility.Instance.SPRITEBUTTON_SIDE_LENGTH / 8, rectangle.Y + GameUtility.Instance.SPRITEBUTTON_SIDE_LENGTH / 8));
        }

        public override void Update(GameTime gameTime)
        {
            //Update the sprite
            this.buttonSprite.Update(gameTime);

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
                    //invoke the event corresponding to the button that was clicked
                    ButtonArgs args = new ButtonArgs();
                    args.gameObject = this.createdObject;
                    Click?.Invoke(this, args);
                }
            }
        }
    }
}
