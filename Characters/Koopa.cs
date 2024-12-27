using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Physics;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class Koopa : IEnemy, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public int health { get; set; }
        public IKoopaStateMachine stateMachine;
        public int Direction { get; set; }
        public ISprite sprite
        {
            get
            {
                return stateMachine.sprite;
            }
        }
        public IPhysics physics { get; set; }
        public int height { get; }
        public int width { get; }

        public Koopa(int x, int y)
        {
            physics = new PhysicsHandler(this);
            movementSpeed = 1;
            health = 3;
            Position = new Vector2(x, y);
            stateMachine = new NonShellKoopaStateMachine(this);
            width = stateMachine.sprite.spriteWidth;
            height = stateMachine.sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public void ChangeDirection()
        {
            stateMachine.ChangeDirection();
        }

        public void BeStomped()
        {
            stateMachine.BeStomped();
            SoundFXStorage.Instance.PlaySoundEffect("stompEnemy");
        }

        public void BeFlipped()
        {
            health -= 2;
            stateMachine.BeFlipped();
            SoundFXStorage.Instance.PlaySoundEffect("stompEnemy");
        }

        public void Update(GameTime gameTime)
        {
            stateMachine.Update(gameTime);
            physics.Update(gameTime);
            UpdateBoundingBox();
        }

        public CollisionType GetCollisionType()
        {
            return stateMachine.GetCollisionType();
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            temp.Width = stateMachine.sprite.spriteWidth;
            temp.Height = stateMachine.sprite.spriteHeight;
            this.BoundingBox = temp;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stateMachine.Draw(spriteBatch);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Koopa((int)location.X, (int)location.Y);
        }
    }
}