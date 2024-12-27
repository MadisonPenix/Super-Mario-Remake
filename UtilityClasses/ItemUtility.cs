namespace TeamMilkGame.UtilityClasses
{
    public class ItemUtility
    {
        private static ItemUtility instance = new ItemUtility();
        public static ItemUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private ItemUtility()
        {

        }

        public int MUSHROOM_WIDTH { get; } = 56;
        public int MUSHROOM_SPEED { get; } = 2;
        public int STAR_SPEED { get; } = 120;
        public int STAR_BOUNCE_SPEED { get; } = 500;
        public int FIREBALL_SPEED { get; } = 7;
        public int FIREBALL_DIRECTION_MULT { get; } = 1;
        public int FIREBALL_BOUNCE_SPEED { get; } = 450;
    }
}
