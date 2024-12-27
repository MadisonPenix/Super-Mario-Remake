using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class BounceStarDown : ICommand
    {
        private InvincibilityStar Star;
        private Rectangle Overlap;
        public BounceStarDown(ICollidable Item, Rectangle overlap)
        {
            this.Star = (InvincibilityStar)Item;
            Overlap = overlap;
        }
        public void Execute()
        {
            Star.physics.Yvelocity = 0;
            Star.Position += new Vector2(0, Overlap.Width);
            Star.UpdateBoundingBox();
        }
    }
}
