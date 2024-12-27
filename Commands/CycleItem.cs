using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TeamMilkGame.ItemClasses;

namespace TeamMilkGame.Commands
{
    internal class CycleItem : ICommand
    {
        private IList<IGameItems> items;
        private MilkGame game;
        private Keys key;
        int pos;
        public CycleItem(MilkGame game, Keys key)
        {
            this.key = key;
            this.game = game;
            this.items = game.gameItems;
            pos = 0;
        }
        public void Execute()
        {
            pos = items.IndexOf(game.item);
            if (key.Equals(Keys.U))
            {
                pos--;
                if (pos < 0)
                {
                    pos = items.Count - 1;
                }
            }
            else
            {
                pos++;
                if (pos >= items.Count)
                {
                    pos = 0;
                }
            }
            game.item = items[pos];
        }
    }
}

