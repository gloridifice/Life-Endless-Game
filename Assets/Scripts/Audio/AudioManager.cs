using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ2023.Audio
{
    [RequireComponent(typeof(AudioPool))]
    public class AudioManager : MonoBehaviour
    {
        private AudioPool audioPool;
        public AudioRootDatabaseObject rootDatabase;

        public static AudioManager Instance { get; private set; }

        private void OnEnable()
        {
  
        }

        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            if (Instance != this)
            {
                Destroy(this);
            }
            rootDatabase.Init();
            audioPool = GetComponent<AudioPool>();
        }

        public AudioData Play(string dbName, string aName)
        {
            AudioDatabaseObject database = rootDatabase.GetDatabase(dbName);
            AudioGroupData groupData = database.GetGroup(aName);
            AudioData audioData = groupData.GetNextAudioData();
            float volume = database.volume * groupData.groupVolume * audioData.volume;
            float pitch = database.pitch * groupData.groupPitch * audioData.pitch;
            bool loop = groupData.groupLoop;
            
            AudioPlayData playData = new AudioPlayData.Builder().Volume(volume).Pitch(pitch).Loop(loop).build();
            audioPool.PutAudio(audioData.audioClip, playData);
            return audioData;
        }

        public IEnumerator WaitForSourceComplete(SoundSource source, float time)
        {
            yield return new WaitForSeconds(time);
            source.isPlaying = false;
        }
    }
}