using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;
using TeamMilkGame.Projectiles;

namespace TeamMilkGame.Commands
{
    internal class BounceProjectile : ICommand
    {
        private IProjectile projectile;
        private Rectangle Overlap;
        public BounceProjectile(ICollidable projectile, Rectangle overlap)
        {
            this.Overlap = overlap;
            this.projectile = (IProjectile)projectile;
        }
        public void Execute()
        {
            projectile.Position = new Vector2(projectile.Position.X, projectile.Position.Y - Overlap.Height);
            projectile.Bounce();
        }
    }
}
