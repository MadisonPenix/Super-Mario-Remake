using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class Goomba : IEnemy, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public float movementSpeed { get; set; }
        public int health { get; set; }
        public int Direction { get; set; }
        public IGoombaState state;
        public ISprite sprite
        {
            get
            {
                return state.sprite;
            }
        }
        public IPhysics physics { get; set; }
        public int height { get; private set; }
        public int width { get; private set; }

        private int GOOMBA_WIDTH = GoombaUtility.Instance.GOOMBA_WIDTH;
        private int GOOMBA_HEIGHT = GoombaUtility.Instance.GOOMBA_HEIGHT;
        private int GOOMBA_STOMPED_HEIGHT = GoombaUtility.Instance.GOOMBA_STOMPED_HEIGHT;
        public int timer { get; set; } = GoombaUtility.Instance.STOMPED_TIMER;

        public Goomba(int x, int y)
        {
            physics = new PhysicsHandler(this);
            movementSpeed = 1;
            health = 1;
            Position = new Vector2(x, y);
            state = new LeftMovingGoombaState(this);
            Direction = GoombaUtility.Instance.INIT_DIRECTION;
            width = GOOMBA_WIDTH;
            height = GOOMBA_HEIGHT;
            BoundingBox = new Rectangle(x + (state.sprite.spriteWidth - width) / 2, y + (state.sprite.spriteHeight - height), this.width, this.height);
        }

        public void ChangeDirection()
        {
            state.ChangeDirection();
            Direction = -Direction;
        }

        public void BeStomped()
        {
            health--;
            SoundFXStorage.Instance.PlaySoundEffect("stompEnemy");
            state.BeStomped();
            height = GOOMBA_STOMPED_HEIGHT;
            BoundingBox = new Rectangle((int)Position.X + (state.sprite.spriteWidth - width) / 2, (int)Position.Y + (state.sprite.spriteHeight - height), this.width, this.height);
        }

        public void BeFlipped()
        {
            health--;
            SoundFXStorage.Instance.PlaySoundEffect("stompEnemy");
            state.BeFlipped();
        }

        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);
            physics.Update(gameTime);
            UpdateBoundingBox();
            if (timer <= 0)
            {
                GameObjectManager.Instance.ReqRemove(this);
            }
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Enemy;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X + (state.sprite.spriteWidth - width) / 2;
            temp.Y = (int)this.Position.Y + (state.sprite.spriteHeight - height);
            this.BoundingBox = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Goomba((int)location.X, (int)location.Y);
        }
    }
}