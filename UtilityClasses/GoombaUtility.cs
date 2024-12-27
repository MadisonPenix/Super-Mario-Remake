namespace TeamMilkGame.UtilityClasses
{
    public class GoombaUtility
    {
        private static GoombaUtility instance = new GoombaUtility();
        public static GoombaUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private GoombaUtility()
        {

        }

        public int GOOMBA_STOMPED_HEIGHT { get; } = 28;
        public int GOOMBA_HEIGHT { get; } = 56;
        public int GOOMBA_WIDTH { get; } = 60;
        public int STOMPED_TIMER { get; } = 10;
        public int INIT_DIRECTION { get; } = -1;
    }
}
