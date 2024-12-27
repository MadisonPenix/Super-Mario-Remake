namespace TeamMilkGame.Commands
{
    internal class ResetCommand : ICommand
    {
        private MilkGame game;
        private IMario mario;
        public ResetCommand(IMario mario, MilkGame game)
        {
            this.game = game;
            this.mario = mario;
        }
        public void Execute()
        {
            //reset mario
            mario.Reset();
        }

    }
}
