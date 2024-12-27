using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class SolidBlock : IBlocks, ICollidable
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }

        public int BlockWidth { get; }
        public int BlockHeight { get; }
        public int width { get; }
        public int height { get; }
        public ISprite sprite { get; }

        public SolidBlock(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = false;
            sprite = SpriteFactory.Instance.CreateSolidBlockSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public void Update(GameTime gameTime)
        {
            // just acts as solid collidable object, not interactable
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        public void Bounce()
        {
            // Nothing
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
            return new SolidBlock((int)location.X, (int)location.Y);
        }
    }
}
