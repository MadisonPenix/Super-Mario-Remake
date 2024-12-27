namespace TeamMilkGame.Commands
{
    internal class CrouchUpCommand : ICommand
    {
        private IMario mario;
        public CrouchUpCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.CrouchUp();
        }
    }
}
