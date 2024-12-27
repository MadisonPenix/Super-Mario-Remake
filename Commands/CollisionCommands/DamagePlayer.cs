using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class DamagePlayer : ICommand
    {
        private IMario mario;
        public DamagePlayer(ICollidable mario, Rectangle overlap)
        {
            this.mario = (IMario)mario;
        }
        public void Execute()
        {
            mario.TakeDamage();
        }
    }
}
