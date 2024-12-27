using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MushroomUp : ICommand
    {
        private IMario mario;
        public MushroomUp(ICollidable mario, Rectangle overlap)
        {
            // Overlap not needed
            this.mario = (IMario)mario;
        }
        public void Execute()
        {
            mario.PowerUp("Mushroom");
        }
    }
}
