using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class InvincibilityStar : IGameItems, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public ISprite sprite { get; set; }
        public IPhysics physics { get; set; }
        public int Direction { get; private set; }
        public int width { get; }
        public int height { get; }

        public InvincibilityStar(int x, int y)
        {
            sprite = SpriteFactory.Instance.CreateStarSprite();
            this.Position = new Vector2(x, y);
            movementSpeed = ItemUtility.Instance.STAR_SPEED;
            physics = new PhysicsHandler(this);
            physics.Xvelocity = movementSpeed;
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
            Direction = 1;
        }

        public void isTouched()
        {
            return;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Star;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            this.BoundingBox = temp;
        }

        public void ChangeDirection()
        {
            Direction = -Direction;
            physics.Xvelocity = -physics.Xvelocity;
        }

        public void Bounce()
        {
            physics.Yvelocity = ItemUtility.Instance.STAR_BOUNCE_SPEED;
        }

        public void Update(GameTime time)
        {
            UpdateBoundingBox();
            sprite.Update(time);
            physics.Update(time);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new InvincibilityStar((int)location.X, (int)location.Y);
        }
    }
}
