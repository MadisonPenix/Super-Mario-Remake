using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamMilkGame.Collision;
using TeamMilkGame.Controllers;

namespace TeamMilkGame
{
    public class DragDrop
    {
        private MouseState currentMouseState;
        private IGameObject draggedItem;
        public Vector2 Position { get; set; }

        public DragDrop(IGameObject item)
        {
            this.draggedItem = item;
            Position = draggedItem.Position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            draggedItem.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            while(currentMouseState.LeftButton == ButtonState.Pressed)
            {
                draggedItem.Position = currentMouseState.Position.ToVector2();
                draggedItem.Update(gameTime);
            }

        }
    }
       
}
