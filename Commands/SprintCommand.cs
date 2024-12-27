namespace TeamMilkGame.Commands
{
    internal class SprintCommand : ICommand
    {
        private IMario mario;
        public SprintCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.Sprint();
        }
    }
}
