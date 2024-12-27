using Microsoft.Xna.Framework;
using TeamMilkGame.MarioStates;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.PlayerStateMachine.MarioStates
{
    public class BigMarioStateMachine : AbstractBigStateMachine
    {
        public BigMarioStateMachine(IMario player, bool faceLeft, bool isRunning, bool isJumping, bool isCrouching)
        {
            this.Player = player;
            this.FaceLeft = faceLeft;
            this.IsRunning = isRunning;
            this.IsJumping = isJumping;
            this.IsCrouching = isCrouching;
            this.IsFlagPole = false;
            createStates();
            SetSprite();
        }

        private void createStates()
        {
            states = new ISprite[2, 2, 2, 2, 2];

            // Jumping Mario sprites
            states[1, 1, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingBigMarioSprite();
            states[0, 1, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingBigMarioSprite();
            states[1, 0, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingBigMarioSprite();
            states[0, 0, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingBigMarioSprite();

            // Running Mario sprites
            states[1, 1, 0, 0, 0] = SpriteFactory.Instance.CreateLeftRunningBigMarioSprite();
            states[0, 1, 0, 0, 0] = SpriteFactory.Instance.CreateRightRunningBigMarioSprite();

            // Idle Mario sprites
            states[1, 0, 0, 0, 0] = SpriteFactory.Instance.CreateLeftFacingBigMarioSprite();
            states[0, 0, 0, 0, 0] = SpriteFactory.Instance.CreateRightFacingBigMarioSprite();

            // Crouching Mario sprites
            states[1, 0, 0, 1, 0] = SpriteFactory.Instance.CreateLeftCrouchingMarioSprite();
            states[0, 0, 0, 1, 0] = SpriteFactory.Instance.CreateRightCrouchingMarioSprite();

            //Flagpole Mario sprites
            states[1, 0, 0, 0, 1] = SpriteFactory.Instance.CreateLeftFlagpoleBigMarioSprite();
            states[0, 0, 0, 0, 1] = SpriteFactory.Instance.CreateRightFlagpoleBigMarioSprite();
        }
        public override void Attack()
        {
            // Nothing
        }
        public override void TakeDamage()
        {
            SoundFXStorage.Instance.PlaySoundEffect("pipe");
            GameObjectManager.Instance.ReqStarChange(Player, true, MarioUtility.Instance.INVINCIBILITY_TIME);
            Player.state = new RegMarioStateMachine(Player, FaceLeft, IsRunning, IsJumping);
        }

    }
}
