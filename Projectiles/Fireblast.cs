using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame.Projectiles
{
    public class Fireblast : IProjectile
    {
        public Rectangle BoundingBox { get; set; }
        private int directionMultiplier;
        public ISprite sprite { get; set; }

        public int width { get; }
        public int height { get; }

        public Vector2 Position { get; set; }
        public Fireblast(Vector2 position, Boolean goLeft)
        {
            sprite = SpriteFactory.Instance.CreateFireblastSprite();
            this.Position = position;
            directionMultiplier = goLeft ? -1 : 1;
            width = 1;
            height = 1;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, this.width, this.height);
        }

        public void Update(GameTime gameTime)
        {
            Position = Position + new Vector2(directionMultiplier * 5, 0);
            sprite.Update(gameTime);
            UpdateBoundingBox();
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        public void Bounce()
        {
            // Does not bounce
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
            //won't ever get called
            return new BrickBlock((int)location.X, (int)location.Y);
        }
    }
}
