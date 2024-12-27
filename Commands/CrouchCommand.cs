namespace TeamMilkGame.Commands
{
    internal class CrouchCommand : ICommand
    {
        private IMario mario;
        public CrouchCommand(IMario mario)
        {

            this.mario = mario;
        }
        public void Execute()
        {
            mario.Crouch();
        }
    }
}
