using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class BouncePlayer : ICommand
    {
        private IMario mario;
        private Rectangle Overlap;
        public BouncePlayer(ICollidable mario, Rectangle overlap)
        {
            // Will never use enemy
            this.mario = (IMario)mario;
            Overlap = overlap;
        }
        public void Execute()
        {
            mario.Position = new Vector2(mario.Position.X, mario.Position.Y - Overlap.Height);
            mario.Down();
            mario.Jump();
            mario.UpdateBoundingBox();
        }
    }
}
