using System.Collections.Generic;

namespace TeamMilkGame.UtilityClasses
{
    public class SoundsUtility
    {
        private static SoundsUtility instance = new SoundsUtility();
        public static SoundsUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private SoundsUtility()
        {

            /*
             * Mario sounds
             */
            soundNamesToFiles.Add("oneUp", "Sounds/1-up");
            soundNamesToFiles.Add("bigJump", "Sounds/big_jump");
            soundNamesToFiles.Add("collectCoin", "Sounds/coin");
            soundNamesToFiles.Add("fireball", "Sounds/fireball");
            soundNamesToFiles.Add("gameOver", "Sounds/game_over");
            soundNamesToFiles.Add("kickShell", "Sounds/kick_shell");
            soundNamesToFiles.Add("marioDeath", "Sounds/mario_death");
            soundNamesToFiles.Add("powerUp", "Sounds/powerup");
            soundNamesToFiles.Add("regularJump", "Sounds/small_jump");

            /*
             * Enemy sounds
             */
            soundNamesToFiles.Add("bowserDeath", "Sounds/bowser_falls");
            soundNamesToFiles.Add("bowserFire", "Sounds/bowser_fire");
            soundNamesToFiles.Add("stompEnemy", "Sounds/stomp");

            /*
             * Block sounds
             */
            soundNamesToFiles.Add("blockBreak", "Sounds/block_break");
            soundNamesToFiles.Add("blockBump", "Sounds/block_bump");
            soundNamesToFiles.Add("pipe", "Sounds/pipe");
            soundNamesToFiles.Add("powerUpAppears", "Sounds/powerup_appears");

            /*
             * Game sounds
             */
            soundNamesToFiles.Add("fireworks", "Sounds/fireworks");
            soundNamesToFiles.Add("flagpole", "Sounds/flagpole");
            soundNamesToFiles.Add("pauseGame", "Sounds/pause");
            soundNamesToFiles.Add("stageClear", "Sounds/stage_clear");
            soundNamesToFiles.Add("timeWarning", "Sounds/time_warning");

            /*
             * Music
             */
            songNamesToFiles.Add("overworldTheme", "Sounds/mario_overworld_theme");
            songNamesToFiles.Add("starmanTheme", "Sounds/starman");
        }

        public Dictionary<string, string> soundNamesToFiles = new Dictionary<string, string>();
        public Dictionary<string, string> songNamesToFiles = new Dictionary<string, string>();

    }
}
