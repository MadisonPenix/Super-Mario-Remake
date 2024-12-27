using TeamMilkGame.MarioStates;

namespace TeamMilkGame.PlayerStateMachine.LuigiStates
{
    public class RegLuigiStateMachine : AbstractRegStateMachine
    {
        public RegLuigiStateMachine(IMario player, bool faceLeft, bool isRunning, bool isJumping)
        {
            this.Player = player;
            this.FaceLeft = faceLeft;
            this.IsRunning = isRunning;
            this.IsJumping = isJumping;
            this.IsFlagPole = false;
            createStates();
            SetSprite();
        }

        private void createStates()
        {
            states = new ISprite[2, 2, 2, 2];

            // Jumping Luigi sprites
            states[1, 1, 1, 0] = SpriteFactory.Instance.CreateLeftJumpingLuigiSprite();
            states[0, 1, 1, 0] = SpriteFactory.Instance.CreateRightJumpingLuigiSprite();
            states[1, 0, 1, 0] = SpriteFactory.Instance.CreateLeftJumpingLuigiSprite();
            states[0, 0, 1, 0] = SpriteFactory.Instance.CreateRightJumpingLuigiSprite();

            // Running Luigi sprites
            states[1, 1, 0, 0] = SpriteFactory.Instance.CreateLeftRunningLuigiSprite();
            states[0, 1, 0, 0] = SpriteFactory.Instance.CreateRightRunningLuigiSprite();

            // Idle Luigi sprites
            states[1, 0, 0, 0] = SpriteFactory.Instance.CreateLeftFacingLuigiSprite();
            states[0, 0, 0, 0] = SpriteFactory.Instance.CreateRightFacingLuigiSprite();

            // Flagpole Luigi sprites
            states[1, 0, 0, 1] = SpriteFactory.Instance.CreateLeftFlagpoleLuigiSprite();
            states[0, 0, 0, 1] = SpriteFactory.Instance.CreateRightFlagpoleLuigiSprite();
        }

        public override void TakeDamage()
        {
            SoundFXStorage.Instance.PlaySoundEffect("marioDeath");
            Player.state = new DeadLuigi(Player);
            Player.lives.LoseLife();
        }
    }
}
