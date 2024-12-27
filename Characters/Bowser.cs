using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.Projectiles;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    // LEGACY CODE. CURRENTLY TEMPORARY UNTIL SPRINT 5
    public class Bowser : IBoss, IPhysicsObject
    {
        public Rectangle BoundingBox { get; set; }
        public Vector2 Position { get; set; }
        public int health { get; set; }
        public float movementSpeed { get; set; }
        public int Direction { get; set; }
        public IBowserState state;

        public ISprite sprite
        {
            get
            {
                return state.sprite;
            }
        }
        public IPhysics physics { get; set; }

        private IList<IProjectile> Projectiles;
        public bool faceLeft;
        private double fireBlastTimer;

        public int width { get; }
        public int height { get; }

        public Bowser(int x, int y)
        {
            physics = new PhysicsHandler(this);
            Direction = 1;
            fireBlastTimer = 0;
            health = 3;
            faceLeft = true;
            Position = new Vector2(x, y);
            state = new LeftMovingBowserState(this);
            Projectiles = new List<IProjectile>();
            width = state.sprite.spriteWidth;
            height = state.sprite.spriteHeight;
            BoundingBox = new Rectangle(x, y, this.width, this.height);
        }

        public void ChangeDirection()
        {
            state.ChangeDirection();
        }

        public void BeStomped()
        {
            //Do nothing, Bowser cannot be stomped 
        }

        public void BeFlipped()
        {
            //Do nothing, Bowser cannot be flipped
        }

        public void Attack()
        {
            SoundFXStorage.Instance.PlaySoundEffect("bowserFire");
            Projectiles.Add(new Fireblast(Position, faceLeft));
        }

        private void UpdateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < Projectiles.Count; i++)
            {
                float XPos = Projectiles[i].Position.X;
                if (XPos <= 0 || XPos >= 800)
                {
                    Projectiles.RemoveAt(i);
                    i--;
                }
                else
                {
                    Projectiles[i].Update(gameTime);
                }
            }
        }

        private void DrawProjectiles(SpriteBatch _spriteBatch)
        {
            foreach (IProjectile projectile in Projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            //Temporary timing of fireblast 
            fireBlastTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (((int)fireBlastTimer % 3) == 0)
            {
                Attack();
                fireBlastTimer = 1;
            }

            //Temporary implementation for enemy direction changes and movement (without collisions)
            if ((Position.X >= 600) || (Position.X <= 200))
            {
                ChangeDirection();
            }
            state.Update(gameTime);
            UpdateProjectiles(gameTime);
            physics.Update(gameTime);
            UpdateBoundingBox();
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Enemy;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            Rectangle temp = this.BoundingBox;
            temp.X = (int)this.Position.X;
            temp.Y = (int)this.Position.Y;
            temp.Width = state.sprite.spriteWidth;
            temp.Height = state.sprite.spriteHeight;
            this.BoundingBox = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            state.Draw(spriteBatch);
            DrawProjectiles(spriteBatch);
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Bowser((int)location.X, (int)location.Y);
        }
    }
}