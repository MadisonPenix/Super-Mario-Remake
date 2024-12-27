using TeamMilkGame.MarioStates;

namespace TeamMilkGame.PlayerStateMachine.MarioStates
{
    public class RegMarioStateMachine : AbstractRegStateMachine
    {
        public RegMarioStateMachine(IMario player, bool faceLeft, bool isRunning, bool isJumping)
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

            // Jumping Mario sprites
            states[1, 1, 1, 0] = SpriteFactory.Instance.CreateLeftJumpingMarioSprite();
            states[0, 1, 1, 0] = SpriteFactory.Instance.CreateRightJumpingMarioSprite();
            states[1, 0, 1, 0] = SpriteFactory.Instance.CreateLeftJumpingMarioSprite();
            states[0, 0, 1, 0] = SpriteFactory.Instance.CreateRightJumpingMarioSprite();

            // Running Mario sprites
            states[1, 1, 0, 0] = SpriteFactory.Instance.CreateLeftRunningMarioSprite();
            states[0, 1, 0, 0] = SpriteFactory.Instance.CreateRightRunningMarioSprite();

            // Idle Mario sprites
            states[1, 0, 0, 0] = SpriteFactory.Instance.CreateLeftFacingMarioSprite();
            states[0, 0, 0, 0] = SpriteFactory.Instance.CreateRightFacingMarioSprite();

            // Flagpole Mario sprites
            states[1, 0, 0, 1] = SpriteFactory.Instance.CreateLeftFlagpoleMarioSprite();
            states[0, 0, 0, 1] = SpriteFactory.Instance.CreateRightFlagpoleMarioSprite();
        }

        public override void TakeDamage()
        {
            SoundFXStorage.Instance.PlaySoundEffect("marioDeath");
            Player.state = new DeadMario(Player);
            Player.lives.LoseLife();
        }
    }
}
