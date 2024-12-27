using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace TeamMilkGame
{
    public class HUD
    {
        public SpriteFont hudFont; // loaded in by LevelLoader
        private string scoreboardTitle;
        private int score;
        private ISprite coinSprite;
        private int coinCount;
        private string levelNumber;
        private double timer;
        private Dictionary<IMario, PlayerLives> playerLives;

        /*private ISprite marioSprite; // placed with numLives in place of text
        private ISprite luigiSprite;*/
        private List<ISprite> spriteList;

        private static HUD instance;
        public static HUD Instance
        {
            get
            {
                return instance;
            }
        }

        public HUD(string worldLevel, double levelTimer)
        {
            instance = this;
            hudFont = MilkGame.gameFont;
            scoreboardTitle = "SCORE";
            score = 0;
            coinCount = 0;
            levelNumber = worldLevel; // read in through XML
            timer = levelTimer; // read in through XML
            playerLives = new Dictionary<IMario, PlayerLives>();
            coinSprite = SpriteFactory.Instance.CreateSmallCoinSprite();
            spriteList = new List<ISprite>
            {
                SpriteFactory.Instance.CreateRightFacingMarioSprite(),
                SpriteFactory.Instance.CreateRightFacingLuigiSprite(),
            };
        }

        public void UpdateScore(int AddedPoints)
        {
            score += AddedPoints;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            coinSprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(hudFont, scoreboardTitle, new Vector2(140, 32), Color.White);
            spriteBatch.DrawString(hudFont, PadWithZeroes(score, 6), new Vector2(140, 64), Color.White);
            int count = 0;
            foreach(IMario player in GameObjectManager.Instance.Players)
            {
                spriteList[count].Draw(spriteBatch, new Vector2(380, 48 + (48 * count)), 2.8f);
                spriteBatch.DrawString(hudFont, "x", new Vector2(435, 65 + (48 * count)), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(hudFont, "" +player.lives.GetLives(), new Vector2(460, 60 + (48 * count)), Color.White);
                count++;
            }
            coinSprite.Draw(spriteBatch, new Vector2(555, 56));
            spriteBatch.DrawString(hudFont, "x", new Vector2(585, 65), Color.White, 0, Vector2.Zero, 0.6f, SpriteEffects.None, 0f);
            spriteBatch.DrawString(hudFont, PadWithZeroes(coinCount, 2), new Vector2(610, 56), Color.White);
            spriteBatch.DrawString(hudFont, "WORLD", new Vector2(720, 32), Color.White);
            spriteBatch.DrawString(hudFont, levelNumber, new Vector2(754, 64), Color.White);
            spriteBatch.DrawString(hudFont, "TIME", new Vector2(1020, 32), Color.White);
            if (timer < 0)
                spriteBatch.DrawString(hudFont, "", new Vector2(1052, 64), Color.White); // no values drawn
            else
                spriteBatch.DrawString(hudFont, PadWithZeroes((int)timer, 3), new Vector2(1049, 64), Color.White);

            // spriteBatch.DrawString(hudFont, "x", new Vector2(682, 118), Color.White);
        }

        // hudValue: either score or coinCount
        // totalLength: amount of digits to be displayed in HUD (2 for coin, 6 for score)
        // ex: score = 104 but HUD needs to display "000104"
        private static string PadWithZeroes(int hudValue, int totalLength)
        {
            int numDigits = hudValue.ToString().Length; // gets number of current digits of int value
            int leadingZeroes = totalLength - numDigits;
            // Repeat() is repeating a given char ('0') a certain amount of times (leadingZeroes)
            return string.Concat(Enumerable.Repeat('0', leadingZeroes)) + hudValue;
        }
    }
}