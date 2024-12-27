using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MovePlayerUp : ICommand
    {
        private IMario mario;
        private Rectangle Overlap;
        public MovePlayerUp(ICollidable mario, Rectangle overlap)
        {
            this.mario = (IMario)mario;
            Overlap = overlap;
        }
        public void Execute()
        {
            mario.Down();
            mario.Position = new Vector2(mario.Position.X, mario.Position.Y - Overlap.Height);
            mario.UpdateBoundingBox();
            mario.physics.Yvelocity = 0;
        }
    }
}
