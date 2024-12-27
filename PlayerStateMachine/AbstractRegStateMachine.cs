using Microsoft.Xna.Framework;
using System;
using TeamMilkGame.Physics;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame.MarioStates
{
    public abstract class AbstractRegStateMachine : IMarioStateMachine
    {
        public IMario Player { get; set; }
        public bool FaceLeft { get; set; }
        public bool IsRunning { get; set; }
        public bool IsJumping { get; set; }
        public bool IsFlagPole { get; set; }
        public bool IsCrouching { get; set; }

        public ISprite[,,,] states { get; set; }

        public void MoveLeft()
        {
            // State change
            if (!(IsRunning && FaceLeft))
            {
                IsRunning = true;
                FaceLeft = true;
                IsCrouching = false;
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
                IsRunning = true;
                FaceLeft = false;
                IsCrouching = false;
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
                SoundFXStorage.Instance.PlaySoundEffect("regularJump");
                IsJumping = true;
                IsCrouching = false;
                SetSprite();
            }
        }

        public void Crouch()
        {
            IsCrouching = true;
            // No animation change
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

        public void Attack()
        {
            // Nothing, can't attack
        }

        public void Down()
        {
            IsJumping = false;
            SetSprite();
        }

        public void DescendFlagpole()
        {
            IsRunning = false;
            IsJumping = false;
            IsCrouching = false;
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
            Player.sprite = states[indices[0], indices[1], indices[2], indices[3]];
        }

        protected int[] GetIndices()
        {
            int[] indices = new int[4];

            indices[0] = FaceLeft ? 1 : 0;
            indices[1] = IsRunning ? 1 : 0;
            indices[2] = IsJumping ? 1 : 0;
            indices[3] = Player.IsFlagPole ? 1 : 0;

            return indices;
        }

    }
}
