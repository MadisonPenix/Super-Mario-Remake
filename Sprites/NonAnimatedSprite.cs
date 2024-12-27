using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.Sprites
{
    public class NonAnimatedSprite : ISprite
    {
        public Texture2D texture { get; private set; }

        public int spriteWidth { get; set; }
        public int spriteHeight { get; set; }
        public float scaleMultiplier;
        private Rectangle sourceRec;
        private Vector2 sourceRectangleSpecs;

        public NonAnimatedSprite(Texture2D texture, Vector2 startTextureLocation, Vector2 endTextureLocation, float scaleMultiplier = 4)
        {
            this.texture = texture;
            this.sourceRectangleSpecs = endTextureLocation - startTextureLocation;
            this.scaleMultiplier = scaleMultiplier;
            spriteWidth = (int)(sourceRectangleSpecs.X * scaleMultiplier);
            spriteHeight = (int)(sourceRectangleSpecs.Y * scaleMultiplier);
            this.sourceRec = new Rectangle((int)startTextureLocation.X, (int)startTextureLocation.Y, (int)sourceRectangleSpecs.X, (int)sourceRectangleSpecs.Y);
        }

        public void Update(GameTime time)
        {
            //Nothing to Update
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Rectangle destRec = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangleSpecs.X * scaleMultiplier), (int)(sourceRectangleSpecs.Y * scaleMultiplier));
            spriteBatch.Draw(texture, destRec, this.sourceRec, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
        {
            Rectangle destRec = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangleSpecs.X * scale), (int)(sourceRectangleSpecs.Y * scale));
            spriteBatch.Draw(texture, destRec, this.sourceRec, Color.White);
        }

        public void DrawWithColor(SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            Rectangle destRec = new Rectangle((int)position.X, (int)position.Y, (int)(sourceRectangleSpecs.X * scaleMultiplier), (int)(sourceRectangleSpecs.Y * scaleMultiplier));
            spriteBatch.Draw(texture, destRec, this.sourceRec, color);
        }
    }
}
