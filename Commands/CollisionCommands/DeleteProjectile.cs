using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class DeleteProjectile : ICommand
    {
        private IGameObject projectile;
        public DeleteProjectile(ICollidable projectile, Rectangle overlap)
        {
            this.projectile = projectile;
        }
        public void Execute()
        {
            GameObjectManager.Instance.ReqRemove(projectile);
        }
    }
}
