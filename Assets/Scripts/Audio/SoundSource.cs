using UnityEngine;

namespace GGJ2023.Audio
{
    public class SoundSource
    {
        private string dbName;
        private string aName;
        public bool isPlaying;
        private AudioManager AudioManager => AudioManager.Instance;
        public SoundSource(string dbName, string aName)
        {
            this.dbName = dbName;
            this.aName = aName;
        }
        public void Play()
        {
            if (!isPlaying)
            {
                AudioData data = AudioManager.Play(dbName, aName);
                isPlaying = true;
                AudioManager.StartCoroutine(AudioManager.WaitForSourceComplete(this, data.audioClip.length));
            }
        }
    }
}