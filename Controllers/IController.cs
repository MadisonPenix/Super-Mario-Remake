using Microsoft.Xna.Framework;

namespace TeamMilkGame.Controllers
{

    internal interface IController
    {
        public void Update(GameTime gametime);
        public void Vibrate();

        public bool HasInput();

    }
}
