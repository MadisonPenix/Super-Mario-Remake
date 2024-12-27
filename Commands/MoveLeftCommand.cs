namespace TeamMilkGame.Commands
{
    internal class MoveLeftCommand : ICommand
    {
        private IMario mario;
        public MoveLeftCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.MoveLeft();
        }
    }
}
