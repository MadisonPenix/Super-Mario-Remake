using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class Coin : IGameItems, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public ISprite sprite { get; set; }
        public int Direction { get; private set; }
        public IPhysics physics { get; set; }
        public int width { get; }
        public int height { get; }

        public Coin(int x, int y)
        {
            sprite = SpriteFactory.Instance.CreateCoinSprite();
            this.Position = new Vector2(x, y);
            movementSpeed = 0;
            Direction = 0;
            physics = new PhysicsHandler(this);
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }
        public void isTouched()
        {
            // Implement scoreboard
            return;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            sprite.Draw(spriteBatch, Position);
        }

        public void ChangeDirection()
        {
            // Nothing
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Coin;
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
            sprite.Update(time);
            UpdateBoundingBox();
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Coin((int)location.X, (int)location.Y);
        }
    }
}
