namespace TeamMilkGame.UtilityClasses
{
    public class CameraUtility
    {
        private static CameraUtility instance = new CameraUtility();
        public static CameraUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private CameraUtility()
        {

        }

        public int X_OFFSET { get; } = 450;
        public int Y_OFFSET { get; } = 1000;
        public float ZOOM { get; } = 0.8f;
        public int SCALE { get; } = 1;
        public int INIT_DIRECTION { get; } = -1;
    }
}
