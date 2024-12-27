using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;

namespace TeamMilkGame
{
    public interface IEnemy : ICollidable
    {
        public float movementSpeed { get; set; }
        public int health { get; set; }
        public int Direction { get; set; }
        void ChangeDirection();
        public IPhysics physics { get; set; }
        public int height { get; }
        public int width { get; }
        void BeStomped();
        void BeFlipped();
        new void Update(GameTime gameTime);
        new void Draw(SpriteBatch spriteBatch);
    }
}