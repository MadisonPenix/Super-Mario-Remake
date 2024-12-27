using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.PlayerStateMachine.MarioStates
{
    public class DeadMario : DeadPlayer
    {
        public DeadMario(IMario player)
        {
            this.Player = player;
            player.sprite = SpriteFactory.Instance.CreateDeadMarioSprite();
            Player.physics.Yvelocity = MarioUtility.Instance.JUMP_SPEED / 1.5;
            Player.physics.Xvelocity = 0;
        }
    }
}
