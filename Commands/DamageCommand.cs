using TeamMilkGame.Controllers;

namespace TeamMilkGame.Commands
{
    internal class DamageCommand : ICommand
    {
        private IMario mario;
        private IController GamepadController;
        private bool isGamepad = false;
        public DamageCommand(IMario mario, IController GamepadController = null)
        {

            this.mario = mario;
            if (GamepadController != null)
            {
                this.GamepadController = GamepadController;
                isGamepad = true;
            }
        }
        public void Execute()
        {
            mario.TakeDamage();
            if (isGamepad)
            {
                GamepadController.Vibrate();
            }
        }
    }
}
