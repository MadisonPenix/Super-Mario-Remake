namespace TeamMilkGame.Commands
{
    internal class PauseCommand : ICommand
    {
        private MilkGame game;

        public PauseCommand(MilkGame game)
        {
            this.game = game;
        }
        public void Execute()
        {
            //switch the bool of paused, if the game is paused unpause and vice versa
            game.paused = !game.paused;

        }

    }
}
