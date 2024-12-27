namespace TeamMilkGame.Commands
{
    internal class AttackCommand : ICommand
    {
        private IMario mario;
        public AttackCommand(IMario mario)
        {
            this.mario = mario;
        }
        public void Execute()
        {
            mario.Attack();
        }
    }
}