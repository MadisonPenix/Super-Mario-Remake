using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class HitBlock : ICommand
    {
        private IBlocks block;
        private Rectangle Overlap;
        public HitBlock(ICollidable block, Rectangle overlap)
        {
            this.block = (IBlocks)block;
            Overlap = overlap;
        }
        public void Execute()
        {
            block.Bounce();
        }
    }
}
