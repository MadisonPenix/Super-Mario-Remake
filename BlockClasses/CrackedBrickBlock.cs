using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class CrackedBrickBlock : IBlocks, ICollidable
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }

        public ISprite sprite { get; }

        public int width { get; }
        public int height { get; }

        public CrackedBrickBlock(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = false;
            sprite = SpriteFactory.Instance.CreateCrackedBrickBlockSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        public void Update(GameTime gameTime)
        {
            //placeholder function, need to implement
        }

        public void Bounce()
        {
            //Nothing
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            // No update for cracked block
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new CrackedBrickBlock((int)location.X, (int)location.Y);
        }
    }
}
