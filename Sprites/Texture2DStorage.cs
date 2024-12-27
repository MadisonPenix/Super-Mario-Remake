using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamMilkGame
{
    public static class Texture2DStorage
    {
        private static Texture2D goombaSpriteSheet;
        private static Texture2D koopaSpriteSheet;
        private static Texture2D blockSpriteSheet;
        private static Texture2D marioSpriteSheet;
        private static Texture2D itemSpriteSheet;
        private static Texture2D bowserSpriteSheet;
        private static Texture2D allSpriteSheet;
        private static Texture2D backgroundSpriteSheet;
        private static Texture2D marioCharSpriteSheet;
        private static Texture2D luigiSpriteSheet;
        public static void LoadAllTextures(ContentManager content)
        {
            goombaSpriteSheet = content.Load<Texture2D>("goomba");
            koopaSpriteSheet = content.Load<Texture2D>("koopa");
            blockSpriteSheet = content.Load<Texture2D>("marioBlocks2");
            marioSpriteSheet = content.Load<Texture2D>("mario");
            itemSpriteSheet = content.Load<Texture2D>("items");
            bowserSpriteSheet = content.Load<Texture2D>("bowser");
            allSpriteSheet = content.Load<Texture2D>("allMarioSprites");
            marioCharSpriteSheet = content.Load<Texture2D>("marioCharacters");
            luigiSpriteSheet = content.Load<Texture2D>("luigi");
            // backgroundSpriteSheet = content.Load<Texture2D>("Super_Mario_1-1");
        }

        public static void UnloadAllTextures()
        {
            // unload all the Texture2Ds - not needed for the scope of this project
        }

        public static Texture2D GetGoombaSpriteSheet()
        {
            return goombaSpriteSheet;
        }

        public static Texture2D GetKoopaSpriteSheet()
        {
            return koopaSpriteSheet;
        }

        public static Texture2D GetBlockSpriteSheet()
        {
            return blockSpriteSheet;
        }

        public static Texture2D GetMarioSpriteSheet()
        {
            return marioSpriteSheet;
        }

        public static Texture2D GetItemSpriteSheet()
        {
            return itemSpriteSheet;
        }

        public static Texture2D GetBowserSpriteSheet()
        {
            return bowserSpriteSheet;
        }

        public static Texture2D GetAllSpriteSheet()
        {
            return allSpriteSheet;
        }

        public static Texture2D GetBackgroundSpriteSheet()
        {
            return backgroundSpriteSheet;
        }

        public static Texture2D GetMarioCharSpriteSheet()
        {
            return marioCharSpriteSheet;
        }
        public static Texture2D GetLuigiSpriteSheet()
        {
            return luigiSpriteSheet;
        }
    }
}
