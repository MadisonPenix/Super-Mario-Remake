using TeamMilkGame.PlayerStateMachine.MarioStates;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.PlayerStateMachine.LuigiStates
{
    public class DeadLuigi : DeadPlayer
    {
        public DeadLuigi(IMario player)
        {
            this.Player = player;
            player.sprite = SpriteFactory.Instance.CreateDeadLuigiSprite();
            Player.physics.Yvelocity = MarioUtility.Instance.JUMP_SPEED / 1.5;
            Player.physics.Xvelocity = 0;
        }
    }
}
