using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.BlockClasses.BlockStates;
using TeamMilkGame.Collision;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame.BlockClasses
{
    public class QuestionBlockInactive : ICollidable, IBlocks
    {
        public Vector2 Position { get; set; }
        public bool Interactable { get; set; }
        public IBlockStateInactive state;


        public QuestionBlockInactive(int x, int y)
        {
            Position = new Vector2(x, y);
            Interactable = false;
            state = new QuestionBlockInactiveState(this);
        }


        public void Update(GameTime gameTime)
        {
            //placeholder function, need to implement
        }
        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Block;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch, Position);
        }
    }
}
