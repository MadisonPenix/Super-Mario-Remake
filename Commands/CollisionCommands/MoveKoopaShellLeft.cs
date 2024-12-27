using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MoveKoopaShellLeft : ICommand
    {
        private IEnemy enemy;
        public MoveKoopaShellLeft(ICollidable enemy, Rectangle overlap)
        {
            // Never use overlap rectangle here
            this.enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            enemy.Position -= new Vector2(10, 0);
            enemy.BeStomped();
        }
    }
}
