using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MovePlayerLeft : ICommand
    {
        private IMario mario;
        private Rectangle Overlap;
        public MovePlayerLeft(ICollidable mario, Rectangle overlap)
        {
            this.mario = (IMario)mario;
            Overlap = overlap;
        }
        public void Execute()
        {
            mario.Position -= new Vector2(Overlap.Width, 0);
            mario.UpdateBoundingBox();
            mario.physics.Xvelocity = 0;
        }
    }
}
