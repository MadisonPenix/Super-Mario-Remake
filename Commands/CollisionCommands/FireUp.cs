using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class FireUp : ICommand
    {
        private IMario mario;
        public FireUp(ICollidable mario, Rectangle overlap)
        {
            // Overlap not needed
            this.mario = (IMario)mario;
        }
        public void Execute()
        {
            mario.PowerUp("Fire");
        }
    }
}
