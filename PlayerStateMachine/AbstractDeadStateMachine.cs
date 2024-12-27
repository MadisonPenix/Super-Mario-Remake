namespace TeamMilkGame.PlayerStateMachine.MarioStates
{
    public abstract class DeadPlayer : IMarioStateMachine
    {
        public IMario Player { get; set; }
        public bool FaceLeft
        {
            get
            {
                return false;
            }
        }
        public bool IsRunning
        {
            get
            {
                return false;
            }
        }
        public bool IsJumping
        {
            get
            {
                return false;
            }
        }
        public bool IsCrouching
        {
            get
            {
                return false;
            }
        }
        public bool IsFlagPole
        {
            get
            {
                return false;
            }
            set
            {
                IsFlagPole = value;
            }
        }

        public void MoveLeft()
        {
            // Nothing, dead
        }

        public void MoveRight()
        {
            // Nothing, dead
        }

        public void Jump()
        {
            // Nothing, dead
        }

        public void Crouch()
        {
            // Nothing, dead
        }

        public void Idle()
        {
            // Nothing, dead
        }

        public void TakeDamage()
        {
            // Nothing, dead
        }

        public void Attack()
        {
            // Nothing, dead
        }

        public void Down()
        {
            // Nothing, dead
        }

        public void DescendFlagpole()
        {
            //Nothing, dead
        }

        public void Update()
        {
            // Nothing to update
        }
    }
}
