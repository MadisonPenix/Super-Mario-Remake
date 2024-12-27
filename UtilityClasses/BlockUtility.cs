using Microsoft.Xna.Framework;

namespace TeamMilkGame.UtilityClasses
{
    public class BlockUtility
    {
        private static BlockUtility instance = new BlockUtility();
        public static BlockUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private BlockUtility()
        {

        }
        public Vector2 UP_LEFT_CORNER { get; } = new Vector2(-150f, 250f);
        public Vector2 UP_RIGHT_CORNER { get; } = new Vector2(150f, 250f);
        public Vector2 DOWN_LEFT_CORNER { get; } = new Vector2(-100f, 350f);
        public Vector2 DOWN_RIGHT_CORNER { get; } = new Vector2(100f, 350f);

        public int DEBRIS_TIMER { get; } = 150;
        public int DEBRIS_OFFSET { get; } = 8;

    }
}
