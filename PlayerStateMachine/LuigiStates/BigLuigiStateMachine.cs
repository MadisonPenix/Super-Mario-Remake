using Microsoft.Xna.Framework;
using TeamMilkGame.MarioStates;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.PlayerStateMachine.LuigiStates
{
    public class BigLuigiStateMachine : AbstractBigStateMachine
    {
        public BigLuigiStateMachine(IMario player, bool faceLeft, bool isRunning, bool isJumping, bool isCrouching)
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
            states[1, 1, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingBigLuigiSprite();
            states[0, 1, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingBigLuigiSprite();
            states[1, 0, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingBigLuigiSprite();
            states[0, 0, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingBigLuigiSprite();

            // Running Mario sprites
            states[1, 1, 0, 0, 0] = SpriteFactory.Instance.CreateLeftRunningBigLuigiSprite();
            states[0, 1, 0, 0, 0] = SpriteFactory.Instance.CreateRightRunningBigLuigiSprite();

            // Idle Mario sprites
            states[1, 0, 0, 0, 0] = SpriteFactory.Instance.CreateLeftFacingBigLuigiSprite();
            states[0, 0, 0, 0, 0] = SpriteFactory.Instance.CreateRightFacingBigLuigiSprite();

            // Crouching Mario sprites
            states[1, 0, 0, 1, 0] = SpriteFactory.Instance.CreateLeftCrouchingLuigiSprite();
            states[0, 0, 0, 1, 0] = SpriteFactory.Instance.CreateRightCrouchingLuigiSprite();

            //Flagpole Mario sprites
            states[1, 0, 0, 0, 1] = SpriteFactory.Instance.CreateLeftFlagpoleBigLuigiSprite();
            states[0, 0, 0, 0, 1] = SpriteFactory.Instance.CreateRightFlagpoleBigLuigiSprite();
        }

        public override void Attack()
        {
            // Nothing
        }

        public override void TakeDamage()
        {
            SoundFXStorage.Instance.PlaySoundEffect("pipe");
            GameObjectManager.Instance.ReqStarChange(Player, true, MarioUtility.Instance.INVINCIBILITY_TIME);
            Player.state = new RegLuigiStateMachine(Player, FaceLeft, IsRunning, IsJumping);
        }
    }
}
