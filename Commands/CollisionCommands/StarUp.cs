using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.Commands
{
    internal class StarUp : ICommand
    {
        private IMario mario;
        public StarUp(ICollidable mario, Rectangle overlap)
        {
            this.mario = (IMario)mario;
        }
        public void Execute()
        {
            GameObjectManager.Instance.ReqStarChange(mario, true, MarioUtility.Instance.STAR_TIMER);
        }
    }
}
