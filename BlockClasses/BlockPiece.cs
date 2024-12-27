using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Physics;

namespace TeamMilkGame
{
    public class BlockPiece : IPhysicsObject
    {
        private ISprite sprite;

        public IPhysics physics { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 accelAmount { get; set; }
        public int width { get; }
        public int height { get; }

        public BlockPiece(int x, int y, Vector2 acceleration)
        {
            Position = new Vector2(x, y);
            sprite = SpriteFactory.Instance.CreateBrickBlockBrokenSprite();
            width = sprite.spriteWidth;
            height = sprite.spriteHeight;
            physics = new PhysicsHandler(this);
            accelAmount = acceleration;
        }

        public void Update(GameTime gameTime)
        {
            physics.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
        }
    }
}
