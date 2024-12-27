using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MovePlayerDown : ICommand
    {
        private IMario mario;
        private Rectangle Overlap;
        public MovePlayerDown(ICollidable mario, Rectangle overlap)
        {
            this.mario = (IMario)mario;
            Overlap = overlap;
        }
        public void Execute()
        {
            // Temporary until physics
            mario.Position += new Vector2(0, Overlap.Height);
            mario.UpdateBoundingBox();
            mario.physics.Yvelocity = 0;
        }
    }
}
