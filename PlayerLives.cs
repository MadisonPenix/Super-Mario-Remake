namespace TeamMilkGame
{
    public class PlayerLives
    {
        private int lives = 3;
        private bool gameOver;
        private bool lifeLost;
        public IMario mario;
        public Camera camera { get; set; }

        public PlayerLives(IMario Mario)
        {
            this.mario = Mario;
            gameOver = false;
            lifeLost = false;
        }
        /*
         * Simply returns the number of lives mario currently has. Mainly used by the HUD
         */
        public int GetLives()
        {
            return lives;
        }

        /*
         * reduces the number of lives mario has by one
         * Once lives hits zero, the number of lives resets to 3 since we have reached game over
         */
        public void LoseLife()
        {
            lives--;
            lifeLost = true;
            if (lives == 0)
            {
                lives = 3;
                gameOver = true;
            }
            else
            {

            }
        }
        /*
         * Returns the value of lostLife
         */
        public bool LifeLost()
        {
            return lifeLost;
        }
        /*
         * Returns the value of gameOver
         */
        public bool GameOver()
        {
            return gameOver;
        }

        /*
         * sets Mario back to the start when he loses a life
         */
        public void Respawn()
        {
            lifeLost = false;
            mario.Reset();
        }

        /*
         * increments the number of lives by one for when we reach 100 coins
         */
        public void GainLife()
        {
            lives++;
        }
    }

}
