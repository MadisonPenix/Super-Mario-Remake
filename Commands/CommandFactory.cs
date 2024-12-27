using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Windows.Input;

namespace TeamMilkGame
{
    public class CommandFactory
    {
        private IDictionary<Keys, ICommand> Commands;

        private static CommandFactory instance = new CommandFactory();

        public static CommandFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private CommandFactory()
        {

        }

        public void LoadAllCommands()
        {

            // More Content.Load calls follow
            //...
        }

        public IDictionary<Keys, ICommand> GetCommands()
        {
            return Commands;
        }
    }
}