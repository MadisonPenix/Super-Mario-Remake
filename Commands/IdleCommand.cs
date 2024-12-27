namespace TeamMilkGame.Commands
{
    internal class IdleCommand : ICommand
    {
        private IMario mario;
        public IdleCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.Idle();
        }
    }
}
