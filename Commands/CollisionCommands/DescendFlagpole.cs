using Microsoft.Xna.Framework;
using TeamMilkGame.Collision;

namespace TeamMilkGame.Commands
{
    internal class DescendFlagpole : ICommand
    {
        private IMario mario;
        public DescendFlagpole(ICollidable mario, Rectangle overlap)
        {
            // Overlap not needed
            this.mario = (IMario)mario;
        }
        public void Execute()
        {
            mario.DescendFlagpole();
            //Make the flagpole no longer collidable but still drawable after it is hit
            foreach (ICollidable obj in GameObjectManager.Instance.MovingCollidables)
            {
                //Need different implementation if multiple marios can collide with the flagpole at different times
                if (obj.GetCollisionType() == ICollidable.CollisionType.Flagpole)
                {
                    GameObjectManager.Instance.ReqRemove(obj);
                    GameObjectManager.Instance.ReqAdd("NonCollidables", obj);
                    break; //Can only be 1 flagpole per level
                }
            }
        }
    }
}
