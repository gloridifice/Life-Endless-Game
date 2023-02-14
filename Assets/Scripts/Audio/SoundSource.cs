using UnityEngine;

namespace GGJ2023.Audio
{
    public class SoundSource
    {
        private string dbName;
        private string aName;
        public bool isPlaying;
        public AudioController currentAudioController;
        private AudioPlayData.Builder playData;
        private AudioManager AudioManager => AudioManager.Instance;
        public SoundSource(string dbName, string aName, AudioPlayData.Builder playData = null)
        {
            this.dbName = dbName;
            this.aName = aName;
            this.playData = playData;
            
            playData = playData ?? new AudioPlayData.Builder();
        }
        public void Play()
        {
            if (!isPlaying && currentAudioController == null)
            {
                AudioController controller = AudioManager.Play(dbName, aName, playData);
                currentAudioController = controller;
                isPlaying = true;
            }
        }

        public void Stop()
        {
            if (isPlaying)
            {
                if (currentAudioController != null)
                {
                    currentAudioController.onDie += OnControllerDie;
                    currentAudioController.Stop();
                }
                else
                {
                    OnControllerDie();
                }
                
            }
        }
        void OnControllerDie()
        {
            this.isPlaying = false;
            currentAudioController.onDie -= OnControllerDie;
            currentAudioController = null;
        }
    }
}