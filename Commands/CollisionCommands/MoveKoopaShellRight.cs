using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class MoveKoopaShellRight : ICommand
    {
        private IEnemy enemy;
        public MoveKoopaShellRight(ICollidable enemy, Rectangle overlap)
        {
            // Never use overlap rectangle here
            this.enemy = (IEnemy)enemy;
        }
        public void Execute()
        {
            enemy.Position += new Vector2(10, 0);
            enemy.BeStomped();
        }
    }
}
