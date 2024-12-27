using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame.Projectiles
{
    public class Fireball : IProjectile, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        private int directionMultiplier;

        public IPhysics physics { get; set; }

        public ISprite sprite { get; set; }

        public Vector2 Position { get; set; }

        public int width { get; }
        public int height { get; }
        public Fireball(Vector2 position, Boolean goLeft)
        {
            physics = new PhysicsHandler(this);
            sprite = SpriteFactory.Instance.CreateFireballSprite();
            this.Position = position;
            directionMultiplier = goLeft ? -ItemUtility.Instance.FIREBALL_DIRECTION_MULT : ItemUtility.Instance.FIREBALL_DIRECTION_MULT;
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle((int)position.X, (int)position.Y, this.width, this.height);
        }

        public void Update(GameTime gameTime)
        {
            Position = Position + new Vector2(directionMultiplier * ItemUtility.Instance.FIREBALL_SPEED, 0);
            physics.Update(gameTime);
            sprite.Update(gameTime);
            UpdateBoundingBox();
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Projectile;
        }

        public void Bounce()
        {
            physics.Yvelocity = ItemUtility.Instance.FIREBALL_BOUNCE_SPEED;
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
