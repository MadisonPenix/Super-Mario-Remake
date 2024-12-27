using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class DamageEnemy : ICommand
    {
        private IEnemy enemy;
        public DamageEnemy(ICollidable enemy, Rectangle overlap)
        {
            // Never use overlap rectangle here
            this.enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            enemy.BeStomped();
            if (enemy.health <= 0)
            {
                GameObjectManager.Instance.ReqRemove(enemy);
            }

        }
    }
}
