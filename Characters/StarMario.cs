using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class StarMario : IMario, IPhysicsObject
    {

        private int Timer;
        private CollisionType CollideType;
        public IMario decMario { get; private set; }

        public StarMario(IMario decMario, int Timer)
        {
            this.Timer = Timer;
            CollideType = Timer == MarioUtility.Instance.STAR_TIMER ? ICollidable.CollisionType.StarMario : ICollidable.CollisionType.Mario;
            this.decMario = decMario;
        }

        public PlayerLives lives
        {
            get
            {
                return decMario.lives;
            }
        }

        public float MoveSpeed
        {
            get
            {
                return this.decMario.MoveSpeed;
            }
        }

        public ISprite sprite
        {
            get
            {
                return decMario.sprite;
            }
            set
            {
                decMario.sprite = value;
            }
        }

        public IPhysics physics
        {
            get
            {
                return decMario.physics;
            }
            set
            {
                decMario.physics = value;
            }
        }

        public int height
        {
            get
            {
                return decMario.height;
            }
            set
            {
                decMario.height = value;
            }
        }
        public int width
        {
            get
            {
                return decMario.width;
            }
            set
            {
                decMario.width = value;
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return decMario.BoundingBox;
            }
            set
            {
                decMario.BoundingBox = value;
            }
        }

        public bool IsCrouched
        {
            get
            {
                return decMario.IsCrouched;
            }
        }
        public bool IsFlagPole
        {
            get
            {
                return decMario.IsFlagPole;
            }

            set
            {
                decMario.IsFlagPole = value;
            }
        }

        public void UpdateBoundingBox()
        {
            decMario.UpdateBoundingBox();
        }

        public Vector2 Position
        {
            get
            {
                return decMario.Position;
            }
            set
            {
                decMario.Position = value;
            }
        }
        public IMarioStateMachine state
        {
            get
            {
                return decMario.state;
            }
            set
            {
                decMario.state = value;
            }
        }

        public void Down()
        {
            decMario.Down();
        }

        public bool FaceLeft
        {
            get
            {
                return decMario.FaceLeft;
            }
        }


        public void Jump()
        {
            decMario.Jump();
        }

        public void MoveLeft()
        {
            decMario.MoveLeft();
        }

        public void MoveRight()
        {
            decMario.MoveRight();
        }

        public void Crouch()
        {
            decMario.Crouch();
        }

        public void Idle()
        {
            decMario.Idle();
        }

        public void Attack()
        {
            decMario.Attack();
        }

        public void TakeDamage()
        {
            // Doesn't take damage
        }

        public void DescendFlagpole()
        {
            decMario.DescendFlagpole();
        }

        public void PowerUp(String itemStr)
        {
            decMario.PowerUp(itemStr);
        }

        public void Reset()
        {
            decMario.Reset();
        }
        public void Sprint()
        {
            decMario.Sprint();
        }

        public void CrouchUp()
        {
            decMario.CrouchUp();
        }

        public void Update(GameTime gameTime)
        {
            Timer--;
            if (Timer <= 0)
            {
                RemoveStar();
            }
            decMario.Update(gameTime);
        }

        private void RemoveStar()
        {
            GameObjectManager.Instance.ReqStarChange(this, false, 0);
        }

        public CollisionType GetCollisionType()
        {
            return CollideType;
        }

        // Will need new sprites
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (Timer % MarioUtility.Instance.STAR_FLASH_DELAY == 0)
            {
                decMario.Draw(_spriteBatch);
            }
        }
        public IGameObject Clone(Vector2 location)
        {
            return new StarMario(this.decMario, this.Timer);
        }
    }
}