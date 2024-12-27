using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MoveEnemyUp : ICommand
    {
        private IEnemy Obj;
        private Rectangle Overlap;
        public MoveEnemyUp(ICollidable Obj, Rectangle overlap)
        {
            this.Obj = (IEnemy)Obj;
            Overlap = overlap;
        }
        public void Execute()
        {
            Obj.Position = new Vector2(Obj.Position.X, Obj.Position.Y - Overlap.Height);
            Obj.UpdateBoundingBox();
            Obj.physics.Yvelocity = 0;
        }
    }
}
