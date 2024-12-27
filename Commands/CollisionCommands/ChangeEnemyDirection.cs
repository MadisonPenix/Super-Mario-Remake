using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class ChangeEnemyDirection : ICommand
    {
        private IEnemy Enemy;
        private Rectangle Overlap;
        public ChangeEnemyDirection(ICollidable Enemy, Rectangle overlap)
        {
            this.Enemy = (IEnemy)Enemy;
            Overlap = overlap;
        }
        public void Execute()
        {
            Enemy.ChangeDirection();
            Enemy.Position += new Vector2(Enemy.Direction * Overlap.Width, 0);
            Enemy.UpdateBoundingBox();
        }
    }
}
