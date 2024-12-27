using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class FireFlower : IGameItems, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public ISprite sprite { get; set; }
        public IPhysics physics { get; set; }
        public int Direction { get; private set; }
        public int width { get; }
        public int height { get; }

        public FireFlower(int x, int y)
        {
            this.Position = new Vector2(x, y);
            movementSpeed = 0;
            sprite = SpriteFactory.Instance.CreateFireFlowerSprite();
            physics = new PhysicsHandler(this);
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            Direction = 0;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public void isTouched()
        {

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
            return ICollidable.CollisionType.FireFlower;
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
            physics.Update(time);
            UpdateBoundingBox();
        }
        public IGameObject Clone(Vector2 location)
        {
            return new FireFlower((int)location.X, (int)location.Y);
        }
    }
}
