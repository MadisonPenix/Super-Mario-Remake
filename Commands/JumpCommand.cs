namespace TeamMilkGame.Commands
{
    internal class JumpCommand : ICommand
    {
        private IMario mario;
        public JumpCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.Jump();
        }
    }
}
