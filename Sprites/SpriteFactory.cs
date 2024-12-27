using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Sprites;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame
{
    public class SpriteFactory
    {
        private Texture2D marioSpritesheet;
        private Texture2D goombaSpritesheet;
        private Texture2D itemSpritesheet;
        private Texture2D koopaSpritesheet;
        private Texture2D blockSpritesheet;
        private Texture2D bowserSpritesheet;
        private Texture2D allSpriteSheet;
        private Texture2D marioCharSpriteSheet;
        private Texture2D luigiSpriteSheet;
        private Texture2D backgroundSpritesheet;

        //constants for the sprite sheet
        private int REGULAR_MARIO_HEIGHT = SpriteFactoryUtility.Instance.REGULAR_MARIO_HEIGHT;
        private int REGULAR_MARIO_WIDTH = SpriteFactoryUtility.Instance.REGULAR_MARIO_WIDTH;
        private int BIG_MARIO_HEIGHT = SpriteFactoryUtility.Instance.BIG_MARIO_HEIGHT;
        private int BIG_MARIO_WIDTH = SpriteFactoryUtility.Instance.BIG_MARIO_WIDTH;
        private int BIG_CROUCH_MARIO_HEIGHT = SpriteFactoryUtility.Instance.BIG_CROUCH_MARIO_HEIGHT;
        private int KOOPA_HEIGHT = SpriteFactoryUtility.Instance.KOOPA_HEIGHT;
        private int KOOPA_WIDTH = SpriteFactoryUtility.Instance.KOOPA_WIDTH;
        private int GOOMBA_HEIGHT = SpriteFactoryUtility.Instance.GOOMBA_HEIGHT;
        private int GOOMBA_WIDTH = SpriteFactoryUtility.Instance.GOOMBA_WIDTH;
        private int FIREBALL_WIDTH = SpriteFactoryUtility.Instance.FIREBALL_WIDTH;
        private int FIREBLAST_WIDTH = SpriteFactoryUtility.Instance.FIREBLAST_WIDTH;
        private int FIREBLAST_HEIGHT = SpriteFactoryUtility.Instance.FIREBLAST_HEIGHT;
        private int BOWSER_WIDTH = SpriteFactoryUtility.Instance.BOWSER_WIDTH;
        private int BLOCK_WIDTH = SpriteFactoryUtility.Instance.BLOCK_WIDTH;
        private int COIN_WIDTH = SpriteFactoryUtility.Instance.COIN_WIDTH;
        private int COIN_HEIGHT = SpriteFactoryUtility.Instance.COIN_HEIGHT;
        private int SMALL_COIN_WIDTH = SpriteFactoryUtility.Instance.SMALL_COIN_WIDTH;
        private int SMALL_COIN_HEIGHT = SpriteFactoryUtility.Instance.SMALL_COIN_HEIGHT;
        private int FLAGPOLE_WIDTH = SpriteFactoryUtility.Instance.FLAGPOLE_WIDTH;
        private int FLAGPOLE_HEIGHT = SpriteFactoryUtility.Instance.FLAGPOLE_HEIGHT;

        private int MARIO_TEXTURE_OFFSET_WIDTH = SpriteFactoryUtility.Instance.MARIO_TEXTURE_OFFSET_WIDTH;
        private int MARIO_TEXTURE_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.MARIO_TEXTURE_OFFSET_HEIGHT;
        private int BIGMARIO_TEXTURE_OFFSET_WIDTH = SpriteFactoryUtility.Instance.BIGMARIO_TEXTURE_OFFSET_WIDTH;
        private int BIGMARIO_TEXTURE_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.BIGMARIO_TEXTURE_OFFSET_HEIGHT;
        private int FIREMARIO_TEXTURE_OFFSET_WIDTH = SpriteFactoryUtility.Instance.FIREMARIO_TEXTURE_OFFSET_WIDTH;
        private int KOOPA_OFFSET_WIDTH = SpriteFactoryUtility.Instance.KOOPA_OFFSET_WIDTH;
        private int KOOPA_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.KOOPA_OFFSET_HEIGHT;
        private int GOOMBA_OFFSET_WIDTH = SpriteFactoryUtility.Instance.GOOMBA_OFFSET_WIDTH;
        private int GOOMBA_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.GOOMBA_OFFSET_HEIGHT;
        private int BOWSER_OFFSET_WIDTH = SpriteFactoryUtility.Instance.BOWSER_OFFSET_WIDTH;
        private int BOWSER_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.BOWSER_OFFSET_HEIGHT;
        private int FIREBALL_OFFSET_WIDTH = SpriteFactoryUtility.Instance.FIREBALL_OFFSET_WIDTH;
        private int FIREBALL_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.FIREBALL_OFFSET_HEIGHT;
        private int FIREBLAST_OFFSET_WIDTH = SpriteFactoryUtility.Instance.FIREBLAST_OFFSET_WIDTH;
        private int FIREBLAST_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.FIREBLAST_OFFSET_HEIGHT;
        private int BLOCK_OFFSET_WIDTH = SpriteFactoryUtility.Instance.BLOCK_OFFSET_WIDTH;
        private int BLOCK_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.BLOCK_OFFSET_HEIGHT;
        private int STAR_OFFSET_WIDTH = SpriteFactoryUtility.Instance.STAR_OFFSET_WIDTH;
        private int STAR_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.STAR_OFFSET_HEIGHT;
        private int COIN_OFFSET_WIDTH = SpriteFactoryUtility.Instance.COIN_OFFSET_WIDTH;
        private int COIN_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.COIN_OFFSET_HEIGHT;
        private int SMALL_COIN_OFFSET_WIDTH = SpriteFactoryUtility.Instance.SMALL_COIN_OFFSET_WIDTH;
        private int SMALL_COIN_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.SMALL_COIN_OFFSET_HEIGHT;
        private int FIREFLOW_OFFSET_WIDTH = SpriteFactoryUtility.Instance.FIREFLOW_OFFSET_WIDTH;
        private int FIREFLOW_OFFSET_HEIGHT = SpriteFactoryUtility.Instance.FIREFLOW_OFFSET_HEIGHT;

        private double FRAMETIME = SpriteFactoryUtility.Instance.FRAMETIME;

        private Vector2 REGULAR_MARIO_SPECS;
        private Vector2 BIG_MARIO_SPECS;
        private Vector2 GOOMBA_SPECS;
        private Vector2 KOOPA_SPECS;

        private int[] RIGHT_RUN_MARIO_INFO = SpriteFactoryUtility.Instance.RIGHT_RUN_MARIO_INFO;
        private int[] LEFT_RUN_MARIO_INFO = SpriteFactoryUtility.Instance.LEFT_RUN_MARIO_INFO;
        private int[] GOOMBA_INFO = SpriteFactoryUtility.Instance.GOOMBA_INFO;
        private int[] LEFT_KOOPA_INFO = SpriteFactoryUtility.Instance.LEFT_KOOPA_INFO;
        private int[] RIGHT_KOOPA_INFO = SpriteFactoryUtility.Instance.RIGHT_KOOPA_INFO;
        private int[] LEFT_BOWSER_INFO = SpriteFactoryUtility.Instance.LEFT_BOWSER_INFO;
        private int[] RIGHT_BOWSER_INFO = SpriteFactoryUtility.Instance.RIGHT_BOWSER_INFO;
        private int[] Q_BLOCK_INFO = SpriteFactoryUtility.Instance.Q_BLOCK_INFO;
        private int[] STAR_INFO = SpriteFactoryUtility.Instance.STAR_INFO;
        private int[] COIN_INFO = SpriteFactoryUtility.Instance.COIN_INFO;
        private int[] SMALL_COIN_INFO = SpriteFactoryUtility.Instance.SMALL_COIN_INFO;
        private int[] FIRE_INFO = SpriteFactoryUtility.Instance.FIRE_INFO;
        private int[] FIREBALL_INFO = SpriteFactoryUtility.Instance.FIREBALL_INFO;
        private int[] FIREBLAST_INFO = SpriteFactoryUtility.Instance.FIREBLAST_INFO;

        public static SpriteFactory Instance { get; } = new SpriteFactory();

        private SpriteFactory()
        {
            REGULAR_MARIO_SPECS = new Vector2(REGULAR_MARIO_WIDTH, REGULAR_MARIO_HEIGHT);
            BIG_MARIO_SPECS = new Vector2(BIG_MARIO_WIDTH, BIG_MARIO_HEIGHT);
            GOOMBA_SPECS = new Vector2(GOOMBA_WIDTH, GOOMBA_HEIGHT);
            KOOPA_SPECS = new Vector2(KOOPA_WIDTH, KOOPA_HEIGHT);
        }

        public void LoadAllTextures()
        {
            goombaSpritesheet = Texture2DStorage.GetGoombaSpriteSheet();
            koopaSpritesheet = Texture2DStorage.GetKoopaSpriteSheet();
            blockSpritesheet = Texture2DStorage.GetBlockSpriteSheet();
            marioSpritesheet = Texture2DStorage.GetMarioSpriteSheet();
            bowserSpritesheet = Texture2DStorage.GetBowserSpriteSheet();
            itemSpritesheet = Texture2DStorage.GetItemSpriteSheet();
            allSpriteSheet = Texture2DStorage.GetAllSpriteSheet();
            marioCharSpriteSheet = Texture2DStorage.GetMarioCharSpriteSheet();
            luigiSpriteSheet = Texture2DStorage.GetLuigiSpriteSheet();
            // backgroundSpritesheet = Texture2DStorage.GetBackgroundSpriteSheet();
        }

        // Regular Mario Sprites -------------------------------------------------------------
        public ISprite CreateRightFacingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateRightRunningMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_RUN_MARIO_LOC;
            int cols = RIGHT_RUN_MARIO_INFO[0];
            int rows = RIGHT_RUN_MARIO_INFO[1];
            bool flippedAnimation = RIGHT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(REGULAR_MARIO_WIDTH * cols, REGULAR_MARIO_HEIGHT * rows)), FRAMETIME / 2, MARIO_TEXTURE_OFFSET_HEIGHT, MARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateRightJumpingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_JUMP_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateLeftFacingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateLeftRunningMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_RUN_MARIO_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(REGULAR_MARIO_WIDTH * cols, REGULAR_MARIO_HEIGHT * rows)), FRAMETIME / 2, MARIO_TEXTURE_OFFSET_HEIGHT, MARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLeftJumpingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_JUMP_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
        public ISprite CreateLeftFlagpoleMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FLAGPOLE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
        public ISprite CreateRightFlagpoleMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FLAGPOLE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }


        // Super Mario Sprites ---------------------------------------------------------------
        public ISprite CreateLeftCrouchingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_CROUCH_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_CROUCH_MARIO_HEIGHT)));
        }

        public ISprite CreateRightCrouchingMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_CROUCH_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_CROUCH_MARIO_HEIGHT)));
        }

        public ISprite CreateLeftFacingBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightFacingBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateLeftRunningBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_RUN_BIG_MARIO_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, BIGMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateRightRunningBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_RUN_BIG_MARIO_LOC;
            int cols = RIGHT_RUN_MARIO_INFO[0];
            int rows = RIGHT_RUN_MARIO_INFO[1];
            bool flippedAnimation = RIGHT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, BIGMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLeftJumpingBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_JUMP_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightJumpingBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_JUMP_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateLeftFlagpoleBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FLAGPOLE_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateRightFlagpoleBigMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FLAGPOLE_BIG_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        // Fire Mario Sprites ----------------------------------------------------------------
        public ISprite CreateLeftCrouchingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_CROUCH_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_MARIO_HEIGHT - 10)));
        }

        public ISprite CreateRightCrouchingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_CROUCH_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_MARIO_HEIGHT - 10)));
        }

        public ISprite CreateLeftFacingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightFacingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateLeftRunningFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_RUN_FIRE_MARIO_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, FIREMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateRightRunningFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_RUN_FIRE_MARIO_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(marioSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, FIREMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLeftJumpingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_JUMP_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightJumpingFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_JUMP_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateLeftAttackFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_ATTACK_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightAttackFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_ATTACK_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateLeftFlagpoleFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FLAGPOLE_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateRightFlagpoleFireMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FLAGPOLE_FIRE_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateDeadMarioSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.DEAD_MARIO_LOC;
            return new NonAnimatedSprite(marioSpritesheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }


        // Goomba Sprites ----------------------------------------------------------------
        //FLIPPED GOOMBA SPRITE DOES NOT EXIST AS OF YET
        public ISprite CreateFlippedGoombaSprite()
        {
            return new AnimatedSprite(koopaSpritesheet, 3, 1, new Vector2(0, 0), new Vector2(60, 10));
        }
        public ISprite CreateMovingGoombaSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.MOVING_GOOMBA_LOC;
            int cols = GOOMBA_INFO[0];
            int rows = GOOMBA_INFO[1];
            bool flippedAnimation = GOOMBA_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(goombaSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(GOOMBA_WIDTH * cols, GOOMBA_HEIGHT * rows)), FRAMETIME, GOOMBA_OFFSET_HEIGHT, GOOMBA_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateStompedGoombaSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.STOMPED_GOOMBA_LOC;
            return new NonAnimatedSprite(goombaSpritesheet, startLoc, (startLoc + GOOMBA_SPECS));
        }

        // Koopa Sprites ----------------------------------------------------------------

        public ISprite CreateFlippedKoopaSprite()
        {
            Vector2 startLoc = new Vector2(323, 0);
            int cols = 1;
            int rows = 1;
            return new AnimatedSprite(koopaSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(KOOPA_WIDTH * cols, KOOPA_HEIGHT * rows)), FRAMETIME, KOOPA_OFFSET_HEIGHT, KOOPA_OFFSET_WIDTH);
        }
        public ISprite CreateLeftMovingKoopaSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_KOOPA_LOC;
            int cols = LEFT_KOOPA_INFO[0];
            int rows = LEFT_KOOPA_INFO[1];
            bool flippedAnimation = LEFT_KOOPA_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(koopaSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(KOOPA_WIDTH * cols, KOOPA_HEIGHT * rows)), FRAMETIME, KOOPA_OFFSET_HEIGHT, KOOPA_OFFSET_WIDTH, flippedAnimation);
        }
        public ISprite CreateRightMovingKoopaSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_KOOPA_LOC;
            int cols = RIGHT_KOOPA_INFO[0];
            int rows = RIGHT_KOOPA_INFO[1];
            bool flippedAnimation = RIGHT_KOOPA_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(koopaSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(KOOPA_WIDTH * cols, KOOPA_HEIGHT * rows)), FRAMETIME, KOOPA_OFFSET_HEIGHT, KOOPA_OFFSET_WIDTH, flippedAnimation);
        }
        public ISprite CreateStompedKoopaSprite()
        {
            Vector2 startLoc = new Vector2(114, 0);
            int cols = 1;
            int rows = 1;
            return new AnimatedSprite(koopaSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(KOOPA_WIDTH * cols, KOOPA_HEIGHT * rows)), FRAMETIME, KOOPA_OFFSET_HEIGHT, KOOPA_OFFSET_WIDTH);
        }

        // Bowser Sprites ----------------------------------------------------------------

        public ISprite CreateLeftMovingBowserSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_BOWSER_LOC;
            int cols = LEFT_BOWSER_INFO[0];
            int rows = LEFT_BOWSER_INFO[1];
            bool flippedAnimation = LEFT_BOWSER_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(bowserSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BOWSER_WIDTH * cols, BOWSER_WIDTH * rows)), FRAMETIME, BOWSER_OFFSET_HEIGHT, BOWSER_OFFSET_WIDTH, flippedAnimation);
        }
        public ISprite CreateRightMovingBowserSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_BOWSER_LOC;
            int cols = RIGHT_BOWSER_INFO[0];
            int rows = RIGHT_BOWSER_INFO[1];
            bool flippedAnimation = RIGHT_BOWSER_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(bowserSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BOWSER_WIDTH * cols, BOWSER_WIDTH * rows)), FRAMETIME, BOWSER_OFFSET_HEIGHT, BOWSER_OFFSET_WIDTH, flippedAnimation);
        }

        // Block Sprites ---------------------------------------------------------------

        public ISprite CreateQuestionBlockActiveSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.Q_BLOCK_ACT_LOC;
            int cols = Q_BLOCK_INFO[0];
            int rows = Q_BLOCK_INFO[1];
            bool flippedAnimation = Q_BLOCK_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(blockSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(BLOCK_WIDTH * cols, BLOCK_WIDTH * rows)), FRAMETIME, BLOCK_OFFSET_HEIGHT, BLOCK_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateQuestionBlockInctiveSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.Q_BLOCK_INACT_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH, BLOCK_WIDTH)));
        }

        public ISprite CreateBrickBlockDefaultSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.BRICK_DEFAULT_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH, BLOCK_WIDTH)));
        }

        public ISprite CreateBrickBlockBrokenSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.BRICK_BROKEN_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH / 2, BLOCK_WIDTH / 2)));
        }

        public ISprite CreateCrackedBrickBlockSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.CRACKED_BRICK_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH, BLOCK_WIDTH)));
        }

        /*
        public ISprite CreateCrackedBrickBlockBrokenSprite()
        {
            // No interaction yet
            Vector2 startLoc = new Vector2(372, 101);
            return new NonAnimatedSprite(allSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
        */

        public ISprite CreateSolidBlockSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.SOLID_BLOCK_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH, BLOCK_WIDTH)));
        }

        public ISprite CreatePipeBlockSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.PIPE_LOC;
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(BLOCK_WIDTH * 2, BLOCK_WIDTH * 2)));
        }

        public ISprite CreatePipeSegmentSprite()
        {
            // Magic numbers
            Vector2 startLoc = new Vector2(59, 56);
            return new NonAnimatedSprite(blockSpritesheet, startLoc, (startLoc + new Vector2(28, 16)));
        }

        //temp till values are corrected
        public ISprite CreateUndergroundCrackedBrickBlockSprite()
        {
            Vector2 startLoc = new Vector2(100, 100);
            return new NonAnimatedSprite(allSpriteSheet, startLoc, (startLoc + new Vector2(100, 100)));
        }

        //temp till values are corrected
        public ISprite CreateUndergroundBrickBlockSprite()
        {
            Vector2 startLoc = new Vector2(100, 100);
            return new NonAnimatedSprite(allSpriteSheet, startLoc, (startLoc + new Vector2(100, 100)));
        }

        // Item Sprites ----------------------------------------------------------------

        public ISprite CreateStarSprite()
        {
            // Reusing goomba specs here
            Vector2 startLoc = SpriteFactoryUtility.Instance.STAR_LOC;
            int cols = STAR_INFO[0];
            int rows = STAR_INFO[1];
            bool flippedAnimation = STAR_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(itemSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(GOOMBA_WIDTH * cols, GOOMBA_HEIGHT * rows)), FRAMETIME / 4, STAR_OFFSET_HEIGHT, STAR_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLifeMushroomSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LIFE_MUSH_LOC;
            return new NonAnimatedSprite(itemSpritesheet, startLoc, (startLoc + GOOMBA_SPECS));
        }

        public ISprite CreatePowerMushroomSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.POWER_MUSH_LOC;
            return new NonAnimatedSprite(itemSpritesheet, startLoc, (startLoc + GOOMBA_SPECS));
        }

        public ISprite CreateCoinSprite()
        {
            // Magic numbers
            Vector2 startLoc = SpriteFactoryUtility.Instance.COIN_LOC;
            int cols = COIN_INFO[0];
            int rows = COIN_INFO[1];
            bool flippedAnimation = COIN_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(itemSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(COIN_WIDTH * cols, COIN_HEIGHT * rows)), FRAMETIME / 2, COIN_OFFSET_HEIGHT, COIN_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateSmallCoinSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.SMALL_COIN_LOC;
            int cols = SMALL_COIN_INFO[0];
            int rows = SMALL_COIN_INFO[1];
            bool flippedAnimation = COIN_INFO[2] != 0;
            return new AnimatedSprite(itemSpritesheet, rows, cols, startLoc,
                (startLoc + new Vector2(SMALL_COIN_WIDTH * cols, SMALL_COIN_HEIGHT * rows)), FRAMETIME, SMALL_COIN_OFFSET_HEIGHT,
                SMALL_COIN_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateFireFlowerSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.FIRE_LOC;
            int cols = FIRE_INFO[0];
            int rows = FIRE_INFO[1];
            bool flippedAnimation = FIRE_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(itemSpritesheet, rows, cols, startLoc, (startLoc + new Vector2(GOOMBA_WIDTH * cols, GOOMBA_HEIGHT * rows)), FRAMETIME / 5, FIREFLOW_OFFSET_HEIGHT, FIREFLOW_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateFlagpoleSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.FLAGPOLE_LOC;
            return new NonAnimatedSprite(allSpriteSheet, startLoc, (startLoc + new Vector2(FLAGPOLE_WIDTH, FLAGPOLE_HEIGHT)));
        }

        // Projectile Sprites -----------------------------------------------------------------
        public ISprite CreateFireballSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.FIREBALL_LOC;
            int cols = FIREBALL_INFO[0];
            int rows = FIREBALL_INFO[1];
            bool flippedAnimation = FIREBALL_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(allSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(FIREBALL_WIDTH * cols, FIREBALL_WIDTH * rows)), FRAMETIME / 2, FIREBALL_OFFSET_HEIGHT, FIREBALL_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateFireblastSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.FIREBLAST_LOC;
            int cols = FIREBLAST_INFO[0];
            int rows = FIREBLAST_INFO[1];
            bool flippedAnimation = FIREBLAST_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(allSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(FIREBLAST_WIDTH * cols, FIREBLAST_HEIGHT * rows)), FRAMETIME, FIREBLAST_OFFSET_HEIGHT, FIREBLAST_OFFSET_WIDTH, flippedAnimation);
        }

        // Luigi Sprites ---------------------------------------------------------------------
        // Regular Luigi Sprites -------------------------------------------------------------
        public ISprite CreateRightFacingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateRightRunningLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_RUN_MARIO_LOC;
            int cols = RIGHT_RUN_MARIO_INFO[0];
            int rows = RIGHT_RUN_MARIO_INFO[1];
            bool flippedAnimation = RIGHT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(luigiSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(REGULAR_MARIO_WIDTH * cols, REGULAR_MARIO_HEIGHT * rows)), FRAMETIME / 2, MARIO_TEXTURE_OFFSET_HEIGHT, MARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateRightJumpingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_JUMP_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateLeftFacingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }

        public ISprite CreateLeftRunningLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_RUN_LUIGI_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(luigiSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(REGULAR_MARIO_WIDTH * cols, REGULAR_MARIO_HEIGHT * rows)), FRAMETIME / 2, MARIO_TEXTURE_OFFSET_HEIGHT, MARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLeftJumpingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_JUMP_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
        public ISprite CreateLeftFlagpoleLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FLAGPOLE_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
        public ISprite CreateRightFlagpoleLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FLAGPOLE_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }


        // Super Luigi Sprites ---------------------------------------------------------------
        public ISprite CreateLeftCrouchingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_CROUCH_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_CROUCH_MARIO_HEIGHT)));
        }

        public ISprite CreateRightCrouchingLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_CROUCH_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH, BIG_CROUCH_MARIO_HEIGHT)));
        }

        public ISprite CreateLeftFacingBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightFacingBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateLeftRunningBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_RUN_BIG_LUIGI_LOC;
            int cols = LEFT_RUN_MARIO_INFO[0];
            int rows = LEFT_RUN_MARIO_INFO[1];
            bool flippedAnimation = LEFT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(luigiSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, BIGMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateRightRunningBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_RUN_BIG_LUIGI_LOC;
            int cols = RIGHT_RUN_MARIO_INFO[0];
            int rows = RIGHT_RUN_MARIO_INFO[1];
            bool flippedAnimation = RIGHT_RUN_MARIO_INFO[2] != 0 ? true : false;
            return new AnimatedSprite(luigiSpriteSheet, rows, cols, startLoc, (startLoc + new Vector2(BIG_MARIO_WIDTH * cols, BIG_MARIO_HEIGHT * rows)), FRAMETIME / 2, BIGMARIO_TEXTURE_OFFSET_HEIGHT, BIGMARIO_TEXTURE_OFFSET_WIDTH, flippedAnimation);
        }

        public ISprite CreateLeftJumpingBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_JUMP_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateRightJumpingBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_JUMP_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateLeftFlagpoleBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.LEFT_FLAGPOLE_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }
        public ISprite CreateRightFlagpoleBigLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.RIGHT_FLAGPOLE_BIG_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + BIG_MARIO_SPECS));
        }

        public ISprite CreateDeadLuigiSprite()
        {
            Vector2 startLoc = SpriteFactoryUtility.Instance.DEAD_LUIGI_LOC;
            return new NonAnimatedSprite(luigiSpriteSheet, startLoc, (startLoc + REGULAR_MARIO_SPECS));
        }
    }
}

