namespace TeamMilkGame.Commands
{
    internal class MoveRightCommand : ICommand
    {
        private IMario mario;
        public MoveRightCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.MoveRight();
        }
    }
}
