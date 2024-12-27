using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MoveItemUp : ICommand
    {
        private IGameItems Obj;
        private Rectangle Overlap;
        public MoveItemUp(ICollidable Obj, Rectangle overlap)
        {
            this.Obj = (IGameItems)Obj;
            Overlap = overlap;
        }
        public void Execute()
        {
            Obj.Position = new Vector2(Obj.Position.X, Obj.Position.Y - Overlap.Height);
            Obj.UpdateBoundingBox();
            Obj.physics.Yvelocity = 0;
            if (Obj is InvincibilityStar)
            {
                (Obj as InvincibilityStar).Bounce();
            }
        }
    }
}
