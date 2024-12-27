using System;
using TeamMilkGame.Collision;
using TeamMilkGame.Physics;

namespace TeamMilkGame
{
    public interface IMario : ICollidable
    {
        public ISprite sprite { get; set; }
        public IPhysics physics { get; set; }
        public IMarioStateMachine state { get; set; }

        public PlayerLives lives { get;}

        public int height { get; set; }
        public int width { get; set; }
        public float MoveSpeed { get; }

        public bool FaceLeft { get; }
        public bool IsCrouched { get; }
        public bool IsFlagPole { get; set; }
        void Idle();
        void Sprint();
        void Down();
        void Attack();
        void Jump();
        void MoveLeft();
        void MoveRight();
        void Crouch();
        void CrouchUp();

        void PowerUp(String itemStr);
        void TakeDamage();
        void DescendFlagpole();
        void Reset();
    }
}
