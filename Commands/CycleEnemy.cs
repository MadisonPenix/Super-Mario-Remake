using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TeamMilkGame.Commands
{
    internal class CycleEnemy : ICommand
    {
        private IList<IEnemy> enemies;
        private MilkGame game;
        private Keys key;
        int pos;
        public CycleEnemy(MilkGame game, Keys key)
        {
            this.key = key;
            this.game = game;
            this.enemies = game.enemies;
            pos = 0;
        }
        public void Execute()
        {
            pos = enemies.IndexOf(game.enemy);
            if (key.Equals(Keys.O))
            {
                pos--;
                if (pos < 0)
                {
                    pos = enemies.Count - 1;
                }
            }
            else
            {
                pos++;
                if (pos >= enemies.Count)
                {
                    pos = 0;
                }
            }
            game.enemy = enemies[pos];
        }
    }
}

