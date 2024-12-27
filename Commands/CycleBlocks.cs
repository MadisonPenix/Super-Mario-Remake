using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TeamMilkGame.BlockClasses;

namespace TeamMilkGame.Commands
{
    internal class CycleBlocks : ICommand
    {
        private IList<IBlocks> blocks;
        private Keys key;
        private MilkGame game;
        int pos;
        public CycleBlocks(MilkGame game, Keys key)
        {
            this.key = key;
            this.game = game;
            this.blocks = game.blocks;
            pos = 0;
        }
        public void Execute()
        {
            pos = blocks.IndexOf(game.block);
            if (key.Equals(Keys.T))
            {
                pos--;
                if (pos < 0)
                {
                    pos = blocks.Count - 1;
                }
            }
            else
            {
                pos++;
                if (pos >= blocks.Count)
                {
                    pos = 0;
                }
            }
            game.block = blocks[pos];
        }
    }
}

