using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class DeleteItem : ICommand
    {
        private IGameObject item;
        public DeleteItem(ICollidable item, Rectangle overlap)
        {
            this.item = item;
        }
        public void Execute()
        {
            GameObjectManager.Instance.ReqRemove(item);
        }
    }
}
