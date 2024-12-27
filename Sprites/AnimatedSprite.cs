using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame.Sprites
{
    public class AnimatedSprite : ISprite
    {
        public Texture2D texture { get; set; }
        private int rows;
        private int columns;
        private int widthTextureOffset;
        private int heightTextureOffset;
        private Vector2 textureStartLocation;
        private Vector2 textureEndLocation;
        private float scaleMultiplier;
        public int spriteWidth { get; }
        public int spriteHeight { get; }
        private int rowSpriteHeight;
        private int rowSpriteWidth;
        private Vector2 sourceRectangleSpecs;
        private int currentFrame;
        private int totalFrames;
        private bool isFlipped;
        public double frameLength;
        private double frameTime;

        public AnimatedSprite(Texture2D texture, int rows, int columns, Vector2 startLocation, Vector2 endLocation, double frameLength = 1, int heightTextureOffset = 0, int widthTextureOffset = 0, bool isFlipped = false, float scaleMultiplier = 4)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.isFlipped = isFlipped;
            this.heightTextureOffset = heightTextureOffset;
            this.widthTextureOffset = widthTextureOffset;
            this.textureStartLocation = startLocation;
            this.textureEndLocation = endLocation;
            sourceRectangleSpecs = endLocation - startLocation;
            rowSpriteHeight = (int)sourceRectangleSpecs.Y / this.rows;
            rowSpriteWidth = (int)sourceRectangleSpecs.X / this.columns;
            this.scaleMultiplier = scaleMultiplier;
            spriteWidth = (int)(rowSpriteWidth * scaleMultiplier);
            spriteHeight = (int)(rowSpriteHeight * scaleMultiplier);
            this.frameLength = frameLength;
            this.frameTime = 0;
            totalFrames = this.rows * this.columns;
            if (isFlipped)
            {
                currentFrame = totalFrames - 1;
            }
            else
            {
                currentFrame = 0;
            }
        }

        public void Update(GameTime time)
        {
            //update frame of animation used based on how long the user specified
            frameTime += time.ElapsedGameTime.TotalSeconds;
            if (frameTime > frameLength)
            {
                if (isFlipped)
                {
                    currentFrame--;
                }
                else
                {
                    currentFrame++;
                }
                frameTime -= frameLength;
            }
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
            else if (currentFrame == -1)
            {
                currentFrame = totalFrames - 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawLocation)
        {
            //get current frame of animation used
            int row = currentFrame / columns;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle((int)textureStartLocation.X + (rowSpriteWidth + widthTextureOffset) * column, (int)textureStartLocation.Y + (rowSpriteHeight + heightTextureOffset) * row, rowSpriteWidth, rowSpriteHeight);
            Rectangle destinationRectangle = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, (int)(rowSpriteWidth * scaleMultiplier), (int)(rowSpriteHeight * scaleMultiplier));

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 drawLocation, float scale)
        {
            //get current frame of animation used
            int row = currentFrame / columns;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle((int)textureStartLocation.X + (rowSpriteWidth + widthTextureOffset) * column, (int)textureStartLocation.Y + (rowSpriteHeight + heightTextureOffset) * row, rowSpriteWidth, rowSpriteHeight);
            Rectangle destinationRectangle = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, (int)(rowSpriteWidth * scale), (int)(rowSpriteHeight * scale));

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    


    public void DrawWithColor(SpriteBatch spriteBatch, Vector2 drawLocation, Color color)
        {
            //get current frame of animation used
            int row = currentFrame / columns;
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle((int)textureStartLocation.X + (rowSpriteWidth + widthTextureOffset) * column, (int)textureStartLocation.Y + (rowSpriteHeight + heightTextureOffset) * row, rowSpriteWidth, rowSpriteHeight);
            Rectangle destinationRectangle = new Rectangle((int)drawLocation.X, (int)drawLocation.Y, (int)(rowSpriteWidth * scaleMultiplier), (int)(rowSpriteHeight * scaleMultiplier));

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }
    }
}
