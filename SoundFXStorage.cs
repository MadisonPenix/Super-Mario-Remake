using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using TeamMilkGame.UtilityClasses;

namespace TeamMilkGame
{
    public class SoundFXStorage
    {
        public static SoundFXStorage Instance { get; } = new SoundFXStorage();

        public Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        public Dictionary<string, SoundEffectInstance> Music = new Dictionary<string, SoundEffectInstance>();
        public bool currentlyPlaying = false;

        public void LoadAllSounds(ContentManager content)
        {
            /*
             * Sounds
             */
            foreach (string key in SoundsUtility.Instance.soundNamesToFiles.Keys)
            {
                soundEffects.Add(key, content.Load<SoundEffect>(SoundsUtility.Instance.soundNamesToFiles[key]));
            }

            /*
             * Music
             */
            foreach (string key in SoundsUtility.Instance.songNamesToFiles.Keys)
            {
                Music.Add(key, content.Load<SoundEffect>(SoundsUtility.Instance.songNamesToFiles[key]).CreateInstance());
            }

;
        }

        public void UnloadAllSounds()
        {

        }

        public void PlaySoundEffect(string soundName)
        {
            soundEffects[soundName].Play();
        }

        public void PlaySong(string songName)
        {
            Music[songName].IsLooped = true;
            Music[songName].Play();
        }
        public void StopSong(string songName)
        {
            Music[songName].IsLooped = false;
            Music[songName].Stop();
        }

        public void PauseSong(string songName)
        {
            Music[songName].Pause();
        }
        public void ResumeSong(string songName)
        {
            Music[songName].Resume();
        }
    }
}
