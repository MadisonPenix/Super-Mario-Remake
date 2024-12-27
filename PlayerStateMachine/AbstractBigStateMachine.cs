using TeamMilkGame.UtilityClasses;
using Microsoft.Xna.Framework;
using System;
using TeamMilkGame.Physics;

namespace TeamMilkGame.MarioStates
{
    public abstract class AbstractBigStateMachine : IMarioStateMachine
    {
        public IMario Player { get; set; }
        public bool FaceLeft { get; set; }
        public bool IsRunning { get; set; }
        public bool IsJumping { get; set; }
        public bool IsCrouching { get; set; }
        public bool IsFlagPole { get; set; }

        public ISprite[,,,,] states { get; set; }

        public void MoveLeft()
        {
            // State change
            if (!(IsRunning && FaceLeft))
            {
                IsCrouching = false;
                IsRunning = true;
                FaceLeft = true;
                SetSprite();
            }
            int ForceMultipier = Player.physics.Xvelocity > 0 ? 2 : 1;
            Player.physics.AddForce(new Vector2(-Player.MoveSpeed * ForceMultipier, 0));
        }

        public void MoveRight()
        {
            // State change
            if (!(IsRunning && !FaceLeft))
            {
                IsCrouching = false;
                IsRunning = true;
                FaceLeft = false;
                SetSprite();
            }
            int ForceMultipier = Player.physics.Xvelocity > 0 ? 2 : 1;
            Player.physics.AddForce(new Vector2(Player.MoveSpeed * ForceMultipier, 0));
        }

        public void Jump()
        {
            // State change
            if (!IsJumping)
            {
                Player.physics.Yvelocity = MarioUtility.Instance.JUMP_SPEED;
                SoundFXStorage.Instance.PlaySoundEffect("bigJump");
                IsCrouching = false;
                IsJumping = true;
                SetSprite();
            }
        }

        public void Crouch()
        {
            if (!IsJumping)
            {
                Player.Position = new Vector2(Player.Position.X, Player.Position.Y + MarioUtility.Instance.CROUCH_POS_ADJ);
                IsRunning = false;
                IsCrouching = true;
                SetSprite();
            }
        }

        public void Idle()
        {
            if (!IsJumping)
            {
                // State change
                IsRunning = false;
                IsJumping = false;
                IsCrouching = false;
                SetSprite();
            }
        }

        public abstract void TakeDamage();

        public abstract void Attack();

        public void Down()
        {
            IsJumping = false;
            SetSprite();
        }

        public void DescendFlagpole()
        {
            IsCrouching = false;
            IsRunning = false;
            IsJumping = false;
            Player.IsFlagPole = true;
            SetSprite();
        }

        public void Update()
        {
            if (!IsJumping)
            {
                IsJumping = Math.Abs(Player.physics.Yvelocity) > 1;
            }
        }

        protected void SetSprite()
        {
            // Convert to indices
            int[] indices = GetIndices();

            // Set sprite
            Player.sprite = states[indices[0], indices[1], indices[2], indices[3], indices[4]];
        }

        protected int[] GetIndices()
        {
            int[] indices = new int[5];

            indices[0] = FaceLeft ? 1 : 0;
            indices[1] = IsRunning ? 1 : 0;
            indices[2] = IsJumping ? 1 : 0;
            indices[3] = IsCrouching ? 1 : 0;
            indices[4] = Player.IsFlagPole ? 1 : 0;

            return indices;
        }

    }
}
