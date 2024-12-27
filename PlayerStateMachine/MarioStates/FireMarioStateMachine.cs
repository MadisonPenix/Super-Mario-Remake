using Microsoft.Xna.Framework;
using TeamMilkGame.MarioStates;
using TeamMilkGame.Projectiles;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.PlayerStateMachine.MarioStates
{
    public class FireMarioStateMachine : AbstractBigStateMachine
    {
        private ISprite[] attStates;

        public FireMarioStateMachine(IMario player, bool faceLeft, bool isRunning, bool isJumping, bool isCrouching)
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
            attStates = new ISprite[2];

            // Jumping Mario sprites
            states[1, 1, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingFireMarioSprite();
            states[0, 1, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingFireMarioSprite();
            states[1, 0, 1, 0, 0] = SpriteFactory.Instance.CreateLeftJumpingFireMarioSprite();
            states[0, 0, 1, 0, 0] = SpriteFactory.Instance.CreateRightJumpingFireMarioSprite();

            // Running Mario sprites
            states[1, 1, 0, 0, 0] = SpriteFactory.Instance.CreateLeftRunningFireMarioSprite();
            states[0, 1, 0, 0, 0] = SpriteFactory.Instance.CreateRightRunningFireMarioSprite();

            // Idle Mario sprites
            states[1, 0, 0, 0, 0] = SpriteFactory.Instance.CreateLeftFacingFireMarioSprite();
            states[0, 0, 0, 0, 0] = SpriteFactory.Instance.CreateRightFacingFireMarioSprite();

            // Crouching Mario sprites
            states[1, 0, 0, 1, 0] = SpriteFactory.Instance.CreateLeftCrouchingFireMarioSprite();
            states[0, 0, 0, 1, 0] = SpriteFactory.Instance.CreateRightCrouchingFireMarioSprite();

            // Attacking Mario sprites
            attStates[1] = SpriteFactory.Instance.CreateLeftAttackFireMarioSprite();
            attStates[0] = SpriteFactory.Instance.CreateRightAttackFireMarioSprite();

            //Flagpole Mario sprites
            states[1, 0, 0, 0, 1] = SpriteFactory.Instance.CreateLeftFlagpoleFireMarioSprite();
            states[0, 0, 0, 0, 1] = SpriteFactory.Instance.CreateRightFlagpoleFireMarioSprite();
        }

        public override void TakeDamage()
        {
            SoundFXStorage.Instance.PlaySoundEffect("pipe");
            GameObjectManager.Instance.ReqStarChange(Player, true, MarioUtility.Instance.INVINCIBILITY_TIME);
            Player.state = new RegMarioStateMachine(Player, FaceLeft, IsRunning, IsJumping);
        }

        public override void Attack()
        {
            SoundFXStorage.Instance.PlaySoundEffect("fireball");
            // Custom set sprite
            GameObjectManager.Instance.ReqAdd("MovingCollidables", new Fireball(Player.Position, FaceLeft));
            IsRunning = false;
            Player.physics.Xvelocity = 0;
            int left = FaceLeft ? 1 : 0;
            Player.sprite = attStates[left];
        }
    }
}
