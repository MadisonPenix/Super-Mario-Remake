namespace TeamMilkGame.UtilityClasses
{
    public class MarioUtility
    {
        private static MarioUtility instance = new MarioUtility();
        public static MarioUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private MarioUtility()
        {

        }

        public int JUMP_HEIGHT_LIMIT { get; } = 200;
        public int REGULAR_MARIO_HEIGHT { get; } = 64;
        public int MARIO_WIDTH { get; } = 56;
        public double FLASH_TIME { get; } = .15;
        public int INVINCIBILITY_TIME { get; } = 100;
        public float JUMP_SPEED { get; } = 820;
        public float MOVE_SPEED { get; } = 1020;
        public double MAX_MOVE_SPEED { get; } = 340;
        public int REG_HEALTH { get; } = 1;
        public int MUSHROOM_HEALTH { get; } = 2;
        public int FIRE_HEALTH { get; } = 3;
        public int CROUCH_POS_ADJ { get; } = 40;
        public int STAR_TIMER { get; } = 500;
        public int STAR_FLASH_DELAY { get; } = 4;
        public int KILL_BOX_Y_POS { get; } = 1000;
        public bool INIT_FLASH { get; } = true;
        public bool INIT_JUMP { get; } = false;
        public bool INIT_IS_CROUCH { get; } = false;
        public bool INIT_FACE_LEFT { get; } = false;
        public bool INIT_RISE { get; } = false;
    }
}
