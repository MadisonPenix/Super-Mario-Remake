using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;
using TeamMilkGame.PlayerStateMachine.LuigiStates;
using TeamMilkGame.PlayerStateMachine.MarioStates;
using TeamMilkGame.Projectiles;
using TeamMilkGame.UtilityClasses;
using static TeamMilkGame.Collision.ICollidable;

namespace TeamMilkGame
{
    public class Luigi : IMario, IPhysicsObject
    {

        public PlayerLives lives { get; private set; }
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public Vector2 startPosition { get; set; }

        public IMarioStateMachine state { set; get; }

        public IPhysics physics { get; set; }

        public ISprite sprite { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public bool FaceLeft
        {
            get
            {
                return state.FaceLeft;
            }
        }
        public bool IsCrouched
        {
            get
            {
                return state.IsCrouching;
            }
        }
        public bool IsFlagPole { get; set; }
        public bool IsJumping
        {
            get
            {
                return state.IsJumping;
            }
        }

        public float MoveSpeed { get; private set; }
        private double maxMoveSpeed;

        private int health;

        private IDictionary<String, Type> Items;
        private IDictionary<String, int> healthItem;

        public Luigi(int x, int y)
        {
            lives = new PlayerLives(this);
            this.Position = new Vector2(x, y);
            this.startPosition = new Vector2(x, y);

            physics = new PhysicsHandler(this, true, true);
            Reset();

            this.MoveSpeed = MarioUtility.Instance.MOVE_SPEED;
            this.maxMoveSpeed = MarioUtility.Instance.MAX_MOVE_SPEED;

            BoundingBox = new Rectangle(x + (sprite.spriteWidth - width) / 2, y, this.width, this.height);

            CreateItemDictionary();
        }

        private void CreateItemDictionary()
        {
            Items = new Dictionary<String, Type>();
            healthItem = new Dictionary<String, int>();

            Items.Add("Mushroom", typeof(BigLuigiStateMachine));
            Items.Add("Fire", typeof(FireLuigiStateMachine));
            healthItem.Add("Mushroom", MarioUtility.Instance.MUSHROOM_HEALTH);
            healthItem.Add("Fire", MarioUtility.Instance.FIRE_HEALTH);
        }

        public void Jump()
        {
            state.Jump();
        }

        public void MoveLeft()
        {
            if (IsCrouched) return;
            state.MoveLeft();
        }

        public void MoveRight()
        {
            if (IsCrouched) return;
            state.MoveRight();
        }

        public void Crouch()
        {
            state.Crouch();
        }

        public void Down()
        {
            state.Down();
        }

        public void Idle()
        {
            if (Math.Abs(physics.Xvelocity) > 0.5)
            {
                int ForceMultiplier = (int)(-physics.Xvelocity / (Math.Abs(physics.Xvelocity)));
                physics.AddForce(new Vector2(ForceMultiplier * this.MoveSpeed, 0));
            }
            else
            {
                state.Idle();
            }
        }

        public void Attack()
        {
            state.Attack();
        }
        public void Sprint()
        {
            this.MoveSpeed *= 1.5f;
            this.maxMoveSpeed *= 1.5f;
        }

        public void TakeDamage()
        {
            state.TakeDamage();
            UpdateBoundingBox();
            health = MarioUtility.Instance.REG_HEALTH;
        }

        public void PowerUp(String itemStr)
        {
            SoundFXStorage.Instance.PlaySoundEffect("powerUp");

            if (health == MarioUtility.Instance.REG_HEALTH)
            {
                Position = new Vector2(Position.X, Position.Y - (BoundingBox.Height));
            }

            if (healthItem[itemStr] > health)
            {
                Type[] types = new Type[] { typeof(IMario), typeof(bool), typeof(bool), typeof(bool), typeof(bool) };
                object[] objs = new object[] { this, FaceLeft, false, IsJumping, IsCrouched };
                state = (IMarioStateMachine)Items[itemStr].GetConstructor(types).Invoke(objs);
                health = healthItem[itemStr];
            }

            UpdateBoundingBox();
        }

        public void DescendFlagpole()
        {
            state.DescendFlagpole();
        }

        public void Reset()
        {
            state = new RegLuigiStateMachine(this, MarioUtility.Instance.INIT_FACE_LEFT, false, false);
            health = MarioUtility.Instance.REG_HEALTH;
            Position = startPosition;
            height = MarioUtility.Instance.REGULAR_MARIO_HEIGHT;
            width = MarioUtility.Instance.MARIO_WIDTH;
        }

        public void Update(GameTime gameTime)
        {
            IsFlagPole = state.IsFlagPole;
            state.Update();
            UpdateBoundingBox();
            sprite.Update(gameTime);
            physics.Update(gameTime, this.maxMoveSpeed);
            this.MoveSpeed = MarioUtility.Instance.MOVE_SPEED;
            this.maxMoveSpeed = MarioUtility.Instance.MAX_MOVE_SPEED;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position);
            //call idle after sprite draws to reset movement
            if (!IsCrouched)
            {
                //only call when not crouching to preserve checking first instance of crouch state
                Idle();
            }
        }

        public CollisionType GetCollisionType()
        {
            return ICollidable.CollisionType.Mario;
        }

        //updates bounding box variable
        public void UpdateBoundingBox()
        {
            // Width never changes
            Rectangle temp = this.BoundingBox;
            temp.X = (int)(Position.X + (sprite.spriteWidth - width) / 2);
            temp.Y = (int)this.Position.Y;
            temp.Height = sprite.spriteHeight;
            this.BoundingBox = temp;
        }

        //corrects mario back in place after crouching
        public virtual void CrouchUp()
        {
            if (state is not RegLuigiStateMachine)
            {
                this.Position = new Vector2(this.Position.X, this.Position.Y - MarioUtility.Instance.CROUCH_POS_ADJ);
            }
        }
        public IGameObject Clone(Vector2 location)
        {
            return new Luigi((int)location.X, (int)location.Y);
        }
    }
}