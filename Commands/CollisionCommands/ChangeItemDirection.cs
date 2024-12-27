using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class ChangeItemDirection : ICommand
    {
        private IGameItems Item;
        private Rectangle Overlap;
        public ChangeItemDirection(ICollidable Item, Rectangle overlap)
        {
            this.Item = (IGameItems)Item;
            Overlap = overlap;
        }
        public void Execute()
        {
            Item.ChangeDirection();
            Item.Position += new Vector2(Item.Direction * Overlap.Width, 0);
            Item.UpdateBoundingBox();
        }
    }
}
