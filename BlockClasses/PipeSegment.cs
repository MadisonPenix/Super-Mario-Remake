using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class PipeSegment : IBlocks, ICollidable
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }

        public int BlockWidth { get; } = 32; // magic numbers?
        public int BlockHeight { get; } = 32; // magic numbers?
        //same as question block, stores a level name within it for the game to access it
        public string xmlLevelStored { get; set; }

        public ISprite sprite { get; }
        public int width { get; }
        public int height { get; }

        private Single rotation;


        public PipeSegment(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = true;
            sprite = SpriteFactory.Instance.CreatePipeSegmentSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
            rotation = 0f;
        }

        public PipeSegment(int x, int y, Single rotation)
        {
            Position = new Vector2(x, y);
            Interactable = true;
            sprite = SpriteFactory.Instance.CreatePipeSegmentSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
            this.rotation = rotation;
        }



        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Pipe;
        }

        public void Bounce()
        {
            //nothing, cannot enter segment
        }

        public void Update(GameTime gameTime)
        {
            /*
             * If Mario collides with pipe, check if collision is with "open" side of pipe
             *      If yes, check if there are any directional/movement inputs
             *              1. Movement inputs are being sent and are in the direction "towards" the open part of pipe --> call Activate()
             *              2. Movement inputs are being sent but not in the right direction --> do nothing
             *              3. No movement inputs --> do nothing
             *      If no, just act as solid impassable object
             */

        }

        public void Activate()
        {
            /*
             * Plays Mario entering pipe animation (note: Mario slides towards center of pipe while animation 
             *      plays if not already centered)
             * Transition level: unload current level and load new
             * Freeze Mario's inputs until finished
             */
            // TODO: Mario entering pipe animation
            // TODO: Unload current level, load new level (does PipeBlock.cs need to store levels/screens somewhere?)

            // TODO: Freeze player inputs

        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            this.BoundingBox = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new PipeSegment((int)location.X, (int)location.Y);
        }
    }
}
