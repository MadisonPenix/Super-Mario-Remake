using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class Flagpole : IGameItems, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public ISprite sprite { get; }
        public IPhysics physics { get; set; }
        public int width { get; }
        public int Direction { get; private set; }
        public int height { get; }

        public Flagpole(int x, int y)
        {
            sprite = SpriteFactory.Instance.CreateFlagpoleSprite();
            this.Position = new Vector2(x - SpriteFactoryUtility.Instance.BLOCK_WIDTH * 2, y);
            movementSpeed = 0;
            physics = new PhysicsHandler(this);
            Direction = 0;
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x + (sprite.spriteWidth - width) / 2, y, this.width, this.height);
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
            //Nothing
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Flagpole;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X + SpriteFactoryUtility.Instance.BLOCK_WIDTH * 3;
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
            return new Flagpole((int)location.X, (int)location.Y);
        }
    }
}
