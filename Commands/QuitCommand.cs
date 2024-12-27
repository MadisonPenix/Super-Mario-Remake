namespace TeamMilkGame.Commands
{
    internal class QuitCommand : ICommand
    {
        private MilkGame game;

        public QuitCommand(MilkGame game)
        {
            this.game = game;
        }
        public void Execute()
        {
            game.Exit();
        }

    }
}
