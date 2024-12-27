using System.Numerics;

namespace TeamMilkGame.UtilityClasses
{
    public class SpriteFactoryUtility
    {
        private static SpriteFactoryUtility instance = new SpriteFactoryUtility();
        public static SpriteFactoryUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private SpriteFactoryUtility()
        {

        }

        public int REGULAR_MARIO_HEIGHT { get; private set; } = 16;
        public int REGULAR_MARIO_WIDTH { get; private set; } = 16;
        public int REGULAR_FLAGPOLE_MARIO_WIDTH { get; private set; } = 13;
        public int REGULAR_FLAGPOLE_MARIO_HEIGHT { get; private set; } = 15;
        public int BIG_MARIO_HEIGHT { get; private set; } = 32;
        public int BIG_CROUCH_MARIO_HEIGHT { get; private set; } = 22;
        public int BIG_MARIO_WIDTH { get; private set; } = 18;
        public int BIG_FLAGPOLE_MARIO_WIDTH { get; private set; } = 14;
        public int BIG_FLAGPOLE_MARIO_HEIGHT { get; private set; } = 30;
        public int KOOPA_HEIGHT { get; private set; } = 24;
        public int KOOPA_WIDTH { get; private set; } = 16;
        public int GOOMBA_HEIGHT { get; private set; } = 16;
        public int GOOMBA_WIDTH { get; private set; } = 16;
        public int FIREBALL_WIDTH { get; private set; } = 8;   // Height not needed, it is the same
        public int FIREBLAST_WIDTH { get; private set; } = 25;
        public int FIREBLAST_HEIGHT { get; private set; } = 9;
        public int BOWSER_WIDTH { get; private set; } = 32; // Height not needed, it is the same
        public int BLOCK_WIDTH { get; private set; } = 16;
        public int COIN_WIDTH { get; private set; } = 11;
        public int COIN_HEIGHT { get; private set; } = 15;
        public int SMALL_COIN_WIDTH { get; private set; } = 5;
        public int SMALL_COIN_HEIGHT { get; private set; } = 8;
        public int FLAGPOLE_WIDTH { get; private set; } = 24;
        public int FLAGPOLE_HEIGHT { get; private set; } = 152;

        public int MARIO_TEXTURE_OFFSET_WIDTH { get; private set; } = 14;
        public int MARIO_TEXTURE_OFFSET_HEIGHT { get; private set; } = 8;
        public int BIGMARIO_TEXTURE_OFFSET_WIDTH { get; private set; } = 13;
        public int BIGMARIO_TEXTURE_OFFSET_HEIGHT { get; private set; } = 8;
        public int FIREMARIO_TEXTURE_OFFSET_WIDTH { get; private set; } = 7;
        public int KOOPA_OFFSET_WIDTH { get; private set; } = 4;
        public int KOOPA_OFFSET_HEIGHT { get; private set; } = 0;
        public int GOOMBA_OFFSET_WIDTH { get; private set; } = 3;
        public int GOOMBA_OFFSET_HEIGHT { get; private set; } = 0;
        public int BOWSER_OFFSET_WIDTH { get; private set; } = 3;
        public int BOWSER_OFFSET_HEIGHT { get; private set; } = 0;
        public int FIREBALL_OFFSET_WIDTH { get; private set; } = 3;
        public int FIREBALL_OFFSET_HEIGHT { get; private set; } = 0;
        public int FIREBLAST_OFFSET_WIDTH { get; private set; } = 6;
        public int FIREBLAST_OFFSET_HEIGHT { get; private set; } = 0;
        public int BLOCK_OFFSET_WIDTH { get; private set; } = 2;
        public int BLOCK_OFFSET_HEIGHT { get; private set; } = 0;
        public int STAR_OFFSET_WIDTH { get; private set; } = 5;
        public int STAR_OFFSET_HEIGHT { get; private set; } = 0;
        public int COIN_OFFSET_WIDTH { get; private set; } = 2;
        public int COIN_OFFSET_HEIGHT { get; private set; } = 2;
        public int SMALL_COIN_OFFSET_WIDTH { get; private set; } = 2;
        public int SMALL_COIN_OFFSET_HEIGHT { get; private set; } = 0;
        public int FIREFLOW_OFFSET_WIDTH { get; private set; } = 3;
        public int FIREFLOW_OFFSET_HEIGHT { get; private set; } = 0;

        public double FRAMETIME { get; private set; } = 0.5;

        public Vector2 RIGHT_MARIO_LOC { get; private set; } = new Vector2(209, 0);
        public Vector2 RIGHT_RUN_MARIO_LOC { get; private set; } = new Vector2(239, 0);
        public Vector2 RIGHT_JUMP_MARIO_LOC { get; private set; } = new Vector2(359, 0);
        public Vector2 LEFT_MARIO_LOC { get; private set; } = new Vector2(179, 0);
        public Vector2 LEFT_RUN_MARIO_LOC { get; private set; } = new Vector2(89, 0);
        public Vector2 LEFT_JUMP_MARIO_LOC { get; private set; } = new Vector2(29, 0);
        public Vector2 RIGHT_FLAGPOLE_MARIO_LOC { get; private set; } = new Vector2(331, 30);
        public Vector2 LEFT_FLAGPOLE_MARIO_LOC { get; private set; } = new Vector2(61, 30);

        public Vector2 LEFT_CROUCH_MARIO_LOC { get; private set; } = new Vector2(0, 57);
        public Vector2 RIGHT_CROUCH_MARIO_LOC { get; private set; } = new Vector2(387, 57);
        public Vector2 RIGHT_BIG_MARIO_LOC { get; private set; } = new Vector2(208, 52);
        public Vector2 LEFT_BIG_MARIO_LOC { get; private set; } = new Vector2(178, 52);
        public Vector2 LEFT_RUN_BIG_MARIO_LOC { get; private set; } = new Vector2(88, 52);
        public Vector2 RIGHT_RUN_BIG_MARIO_LOC { get; private set; } = new Vector2(238, 52);
        public Vector2 LEFT_JUMP_BIG_MARIO_LOC { get; private set; } = new Vector2(28, 52);
        public Vector2 RIGHT_JUMP_BIG_MARIO_LOC { get; private set; } = new Vector2(358, 52);
        public Vector2 RIGHT_FLAGPOLE_BIG_MARIO_LOC { get; private set; } = new Vector2(390, 87);
        public Vector2 LEFT_FLAGPOLE_BIG_MARIO_LOC { get; private set; } = new Vector2(1, 87);

        public Vector2 LEFT_CROUCH_FIRE_MARIO_LOC { get; private set; } = new Vector2(0, 127);
        public Vector2 RIGHT_CROUCH_FIRE_MARIO_LOC { get; private set; } = new Vector2(387, 127);
        public Vector2 RIGHT_FIRE_MARIO_LOC { get; private set; } = new Vector2(208, 122);
        public Vector2 LEFT_FIRE_MARIO_LOC { get; private set; } = new Vector2(178, 122);
        public Vector2 LEFT_RUN_FIRE_MARIO_LOC { get; private set; } = new Vector2(101, 122);
        public Vector2 RIGHT_RUN_FIRE_MARIO_LOC { get; private set; } = new Vector2(236, 122);
        public Vector2 LEFT_JUMP_FIRE_MARIO_LOC { get; private set; } = new Vector2(26, 122);
        public Vector2 RIGHT_JUMP_FIRE_MARIO_LOC { get; private set; } = new Vector2(361, 122);
        public Vector2 LEFT_ATTACK_FIRE_MARIO_LOC { get; private set; } = new Vector2(76, 122);
        public Vector2 RIGHT_ATTACK_FIRE_MARIO_LOC { get; private set; } = new Vector2(311, 122);
        public Vector2 RIGHT_FLAGPOLE_FIRE_MARIO_LOC { get; private set; } = new Vector2(390, 158);
        public Vector2 LEFT_FLAGPOLE_FIRE_MARIO_LOC { get; private set; } = new Vector2(1, 158);

        public Vector2 DEAD_MARIO_LOC { get; private set; } = new Vector2(0, 16);

        public Vector2 MOVING_GOOMBA_LOC { get; private set; } = new Vector2(134, 2);
        public Vector2 STOMPED_GOOMBA_LOC { get; private set; } = new Vector2(115, 2);
        public Vector2 LEFT_KOOPA_LOC { get; private set; } = new Vector2(153, 0);
        public Vector2 RIGHT_KOOPA_LOC { get; private set; } = new Vector2(267, 0);
        public Vector2 STOMPED_KOOPA_LOC { get; private set; } = new Vector2(115, 0);
        public Vector2 LEFT_BOWSER_LOC { get; private set; } = new Vector2(4, 2);
        public Vector2 RIGHT_BOWSER_LOC { get; private set; } = new Vector2(144, 2);

        public Vector2 Q_BLOCK_ACT_LOC { get; private set; } = new Vector2(83, 13);
        public Vector2 Q_BLOCK_INACT_LOC { get; private set; } = new Vector2(65, 13);
        public Vector2 BRICK_DEFAULT_LOC { get; private set; } = new Vector2(11, 13);
        public Vector2 BRICK_BROKEN_LOC { get; private set; } = new Vector2(12, 3);
        public Vector2 CRACKED_BRICK_LOC { get; private set; } = new Vector2(29, 13);
        public Vector2 SOLID_BLOCK_LOC { get; private set; } = new Vector2(47, 13);
        public Vector2 FLAGPOLE_LOC { get; private set; } = new Vector2(259, 45);

        public Vector2 PIPE_LOC { get; private set; } = new Vector2(11, 39);
        public Vector2 STAR_LOC { get; private set; } = new Vector2(40, 46);
        public Vector2 LIFE_MUSH_LOC { get; private set; } = new Vector2(32, 3);
        public Vector2 POWER_MUSH_LOC { get; private set; } = new Vector2(51, 3);
        public Vector2 COIN_LOC { get; private set; } = new Vector2(1, 12);
        public Vector2 SMALL_COIN_LOC { get; private set; } = new Vector2(1, 1);
        public Vector2 FIRE_LOC { get; private set; } = new Vector2(32, 25);
        public Vector2 FIREBALL_LOC { get; private set; } = new Vector2(313, 945);
        public Vector2 FIREBLAST_LOC { get; private set; } = new Vector2(55, 937);

        public int[] RIGHT_RUN_MARIO_INFO { get; private set; } = new[] { 3, 1, 0 };
        public int[] LEFT_RUN_MARIO_INFO { get; private set; } = new[] { 3, 1, 1 };
        public int[] GOOMBA_INFO { get; private set; } = new[] { 2, 1, 0 };
        public int[] RIGHT_KOOPA_INFO { get; private set; } = new[] { 2, 1, 0 };
        public int[] LEFT_KOOPA_INFO { get; private set; } = new[] { 2, 1, 0 };
        public int[] RIGHT_BOWSER_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] LEFT_BOWSER_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] Q_BLOCK_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] STAR_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] COIN_INFO { get; private set; } = new[] { 2, 2, 1 };
        public int[] SMALL_COIN_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] FIRE_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] FIREBALL_INFO { get; private set; } = new[] { 4, 1, 0 };
        public int[] FIREBLAST_INFO { get; private set; } = new[] { 2, 1, 0 };

        public Vector2 RIGHT_LUIGI_LOC { get; private set; } = new Vector2(209, 0);
        public Vector2 RIGHT_RUN_LUIGI_LOC { get; private set; } = new Vector2(239, 0);
        public Vector2 RIGHT_JUMP_LUIGI_LOC { get; private set; } = new Vector2(359, 0);
        public Vector2 LEFT_LUIGI_LOC { get; private set; } = new Vector2(179, 0);
        public Vector2 LEFT_RUN_LUIGI_LOC { get; private set; } = new Vector2(89, 0);
        public Vector2 LEFT_JUMP_LUIGI_LOC { get; private set; } = new Vector2(29, 0);
        public Vector2 RIGHT_FLAGPOLE_LUIGI_LOC { get; private set; } = new Vector2(331, 30);
        public Vector2 LEFT_FLAGPOLE_LUIGI_LOC { get; private set; } = new Vector2(61, 30);

        public Vector2 LEFT_CROUCH_LUIGI_LOC { get; private set; } = new Vector2(0, 57);
        public Vector2 RIGHT_CROUCH_LUIGI_LOC { get; private set; } = new Vector2(387, 57);
        public Vector2 RIGHT_BIG_LUIGI_LOC { get; private set; } = new Vector2(208, 52);
        public Vector2 LEFT_BIG_LUIGI_LOC { get; private set; } = new Vector2(178, 52);
        public Vector2 LEFT_RUN_BIG_LUIGI_LOC { get; private set; } = new Vector2(88, 52);
        public Vector2 RIGHT_RUN_BIG_LUIGI_LOC { get; private set; } = new Vector2(238, 52);
        public Vector2 LEFT_JUMP_BIG_LUIGI_LOC { get; private set; } = new Vector2(28, 52);
        public Vector2 RIGHT_JUMP_BIG_LUIGI_LOC { get; private set; } = new Vector2(358, 52);
        public Vector2 RIGHT_FLAGPOLE_BIG_LUIGI_LOC { get; private set; } = new Vector2(390, 87);
        public Vector2 LEFT_FLAGPOLE_BIG_LUIGI_LOC { get; private set; } = new Vector2(1, 87);

        public Vector2 LEFT_CROUCH_FIRE_LUIGI_LOC { get; private set; } = new Vector2(0, 127);
        public Vector2 RIGHT_CROUCH_FIRE_LUIGI_LOC { get; private set; } = new Vector2(387, 127);
        public Vector2 RIGHT_FIRE_LUIGI_LOC { get; private set; } = new Vector2(208, 122);
        public Vector2 LEFT_FIRE_LUIGI_LOC { get; private set; } = new Vector2(178, 122);
        public Vector2 LEFT_RUN_FIRE_LUIGI_LOC { get; private set; } = new Vector2(101, 122);
        public Vector2 RIGHT_RUN_FIRE_LUIGI_LOC { get; private set; } = new Vector2(236, 122);
        public Vector2 LEFT_JUMP_FIRE_LUIGI_LOC { get; private set; } = new Vector2(26, 122);
        public Vector2 RIGHT_JUMP_FIRE_LUIGI_LOC { get; private set; } = new Vector2(361, 122);
        public Vector2 LEFT_ATTACK_FIRE_LUIGI_LOC { get; private set; } = new Vector2(76, 122);
        public Vector2 RIGHT_ATTACK_FIRE_LUIGI_LOC { get; private set; } = new Vector2(311, 122);
        public Vector2 RIGHT_FLAGPOLE_FIRE_LUIGI_LOC { get; private set; } = new Vector2(390, 158);
        public Vector2 LEFT_FLAGPOLE_FIRE_LUIGI_LOC { get; private set; } = new Vector2(1, 158);

        public Vector2 DEAD_LUIGI_LOC { get; private set; } = new Vector2(0, 16);
    }
}