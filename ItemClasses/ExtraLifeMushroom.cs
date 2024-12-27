using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class ExtraLifeMushroom : IGameItems, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public ISprite sprite { get; set; }
        public IPhysics physics { get; set; }
        public int width { get; }
        public int Direction { get; private set; }
        public int height { get; }

        private int WIDTH = ItemUtility.Instance.MUSHROOM_WIDTH;

        public ExtraLifeMushroom(int x, int y)
        {
            physics = new PhysicsHandler(this);
            sprite = SpriteFactory.Instance.CreateLifeMushroomSprite();
            this.Position = new Vector2(x, y);
            movementSpeed = ItemUtility.Instance.MUSHROOM_SPEED;
            physics.Xvelocity = movementSpeed;
            width = WIDTH;
            Direction = 1;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x + (sprite.spriteWidth - width) / 2, y, this.width, this.height);
        }
        public void isTouched()
        {
            /* functionality for Mario getting 1-Up, implement in future
            if (mario.position.LengthSquared == position.LengthSquared)
            {
                IPlayerState currentMario = mario.state;
            }
            */
            return;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }

        public void ChangeDirection()
        {
            Direction = -Direction;
            movementSpeed = -movementSpeed;
            physics.Xvelocity = movementSpeed;
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Mushroom;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            this.BoundingBox = temp;
        }

        public void Update(GameTime time)
        {
            Position = Position + new Vector2(movementSpeed, 0);
            sprite.Update(time);
            physics.Update(time);
            UpdateBoundingBox();
        }
        public IGameObject Clone(Vector2 location)
        {
            return new ExtraLifeMushroom((int)location.X, (int)location.Y);
        }
    }
}
