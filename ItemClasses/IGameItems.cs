using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;

namespace TeamMilkGame
{
    public interface IGameItems : ICollidable
    {
        public float movementSpeed { get; set; }

        public void isTouched();
        public int width { get; }

        public IPhysics physics { get; set; }
        public int height { get; }
        public int Direction { get; }
        public void ChangeDirection();

        new void Update(GameTime time);
        new void Draw(SpriteBatch spriteBatch);
    }
}