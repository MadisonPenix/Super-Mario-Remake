namespace TeamMilkGame
{
    public interface IMarioStateMachine
    {
        public bool FaceLeft { get; }
        public bool IsRunning { get;}
        public bool IsJumping { get;}
        public bool IsCrouching { get; }
        public bool IsFlagPole { get; set; }
        public void MoveLeft();
        public void Idle();
        public void MoveRight();
        public void Jump();
        public void Crouch();
        public void TakeDamage();
        public void Attack();
        public void Down();
        public void Update();
        public void DescendFlagpole();
    }
}
