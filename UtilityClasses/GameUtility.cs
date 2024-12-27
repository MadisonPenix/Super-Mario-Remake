using Microsoft.Xna.Framework;

namespace TeamMilkGame.UtilityClasses
{
    public class GameUtility
    {
        private static GameUtility instance = new GameUtility();
        public static GameUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private GameUtility()
        {

        }
        public Vector2 PAUSE_POS { get; } = new Vector2(2, 2);
        public Vector2 PAUSE_TEXT_OFFSET { get; } = new Vector2(0, 100);
        public Vector2 ARROW_ONE_POS { get; } = new Vector2(835, 425);
        public Vector2 ARROW_TWO_POS { get; } = new Vector2(835, 475);
        public Rectangle BG_RECT { get; } = new Rectangle(1, 60, 176, 88);
        public Rectangle ALT_SPRITEBATCH_RECT { get; } = new Rectangle(2, 2, 50, 50);
        public Rectangle LEVEL_SELECT_SCREEN_RECT { get; } = new Rectangle(1, 61, 324, 167);
        public Rectangle MARIO_SOURCE { get; } = new Rectangle(209, 0, 16, 16);
        public Color BUTTON_COLOR { get; } = new Color(255, 255, 255, 128);

        public int MENU_BUTTON_Y_OFFSET { get; } = 60;
        public int QUIT_BUTTON_Y_OFFSET { get; } = 120;
        public int BUTTON_Y_OFFSET { get; } = 60;
        public int INIT_CAMERA_POS { get; } = 700;
        public int BACKGROUND_WIDTH { get; } = 428;
        public int BG_SOURCE_MAX { get; } = 1536;
        public int ONE_PLAY_BUTTON_Y { get; } = 425;
        public int TWO_PLAY_BUTTON_Y { get; } = 475;
        public int MENU_BUTTON_OFFSET_X { get; } = 230;
        public int LEVEL_SELECT_START_Y { get; } = 225;
        public int LEVEL_SELECT_END_Y { get; } = 600;
        public int LEVEL_SELECT_BUTTON_SPACING { get; } = 60;
        public int LEVEL_SELECT_MAX_PER_PAGE { get; } = 6;
        public int MAIN_MENU_QUIT_BUTTON_Y_OFFSET { get; } = 525;
        public float TITLE_MULT { get; } = 3f;
        public int TITLE_Y_POS { get; } = 96;
        public int GRAPHICS_BUFFER_WIDTH { get; } = 1280;
        public int GRAPHICS_BUFFER_HEIGHT { get; } = 720;
        public int BUTTON_DIM { get; } = 1;
        public int SPRITEBUTTON_SIDE_LENGTH { get; } = 85;

        public int SPRITEBUTTON_SIDE_OFFSET { get; } = 21;
        public int COL_WIDTH { get; } = 132;
        public int CONT_Y_OFFSET { get; } = 80;
        public double LEVEL_DELAY_TIMER { get; } = 2.5;
        public double LEVEL_END_TIMER { get; } = 6;
        public int COLLISION_WIDTH_OFFSET { get; } = 1;
        public int GRID_SIZE { get; } = 64;

        public int BIG_RECT_WIDTH { get; } = 800;
        public int BIG_RECT_HEIGHT { get; } = 310;
        public int SMALL_RECT_WIDTH { get; } = 750;
        public int SMALL_RECT_HEIGHT { get; } = 100;
        public int TOP_TEXT_OFFSET_X { get; } = 350;
        public int TOP_TEXT_OFFSET_Y { get; } = 40;

        public int BIG_RECT_OFFSET_X { get; } = 100;
        public int BIG_RECT_OFFSET_Y { get; } = 50;
        public int SMALL_RECT_OFFSET_X { get; } = 90;
        public int SMALL_RECT_OFFSET_Y { get; } = 40;

        public string MENU_STR { get; } = "MAIN MENU";
        public string QUIT_STR { get; } = "QUIT GAME (Q)";
        public string RESUME_STR { get; } = "RESUME GAME (ESC)";
        public string PAUSE_STR { get; } = "PAUSED";
        public string ONE_PLAY_STR { get; } = "1 PLAYER GAME";
        public string TWO_PLAY_STR { get; } = "2 PLAYER GAME";
        public string LEVEL_ONE_STR { get; } = "LEVEL 1-1";
        public string NEW_LEVEL_STR { get; } = "NEW LEVEL";
        public string LOAD_LEVEL_STR { get; } = "LOAD LEVEL";
        public string CONTINUE_STR { get; } = "CONTINUE";
        public string GAME_OVER_STR { get; } = "GAME OVER";
        public string LEVEL_STR { get; } = "LEVEL ";
        public string SAVE_STR { get; } = "SAVE";
        public string BACK_STR { get; } = "BACK";
        public string LEAVE_STR { get; } = "LEAVE";
        public string QUIT_WO_SAVING_STR { get; } = "LEAVE WITHOUT SAVING?";
        public string NEXT_PAGE_STR { get; } = "NEXT PAGE";
        public string PREV_PAGE_STR { get; } = "PREVIOUS PAGE";
        public string SUBMIT_STR { get; } = "SUBMIT";
        public string LEVEL_ONE_XML_STR { get; } = "Super_Mario_1-1.xml";
        public int MAX_LEVEL_LENGTH { get; } = 200000;

        // Game Transitions

        // frames start at Vector2.Zero so we don't need a utility for that
        public Vector2 MENU_TRANSITION_FRAMES_END { get; } = new Vector2(2568, 1440);
        public float MENU_TRANSITION_FRAMETIME { get; } = 0.04f;
        public Rectangle MENU_TRANSITION_BG_SOURCE { get; } = new Rectangle(2140, 1200, 428, 240);
        public Rectangle MENU_TRANSITION_TITLE_SOURCE { get; } = new Rectangle(2266, 1232, 176, 88);
        public float INITIAL_ALPHA_VAL { get; } = 0;
        public float ALPHA_INCREASE_AMT { get; } = 0.05f;

        /* the switch statement in GameTransitions did not like these values unless I explicitly
         * declared them as constant, so they still work but they're accessed by simply
         * GameUtility.MENU_TRANSITION_[FRAMECOUNT/DELAY] instead of GameUtility.Instance.~ */
        public const int MENU_TRANSITION_FRAMECOUNT = 70;
        // the delay value is extra frames after the transition is over before the menu buttons are drawn
        public const int MENU_TRANSITION_DELAY = 30;


        //Level Editor Lists
        public IGameObject[] BLOCK_LIST = {new BrickBlock(1, 2), new PipeBlock(1, 2), new CrackedBrickBlock(1, 2), new PipeSegment(1, 2),
                new QuestionBlock(1, 2), new SolidBlock(1, 2)};

        public IGameObject[] ENEMY_LIST = {new Koopa(1, 2), new Goomba(1, 2), new Bowser(1, 2)};

        //todo: add background objets to background list
        public IGameObject[] BACKGROUND_LIST = { };
    }
}
