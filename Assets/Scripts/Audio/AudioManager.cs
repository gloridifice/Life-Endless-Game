using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace GGJ2023.Audio
{
    [RequireComponent(typeof(AudioPool))]
    public class AudioManager : MonoBehaviour
    {
        private AudioPool audioPool;
        public AudioRootDatabaseObject rootDatabase;
        public AudioMixer mixer;

        public static AudioManager Instance { get; private set; }

        private SoundSource bgmSoundSource = new ("bgm", "level");

        private void Update()
        {
            if (!bgmSoundSource.isPlaying)
            {
                bgmSoundSource.Play();
            }
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
            AudioMixerGroup group = database.mixerGroup;
            
            AudioPlayData playData = new AudioPlayData.Builder().Volume(volume).Pitch(pitch).Loop(loop).MixerGroup(group).build();
            audioPool.PutAudio(audioData.audioClip, playData);
            return audioData;
        }

        public IEnumerator WaitForSourceComplete(SoundSource source, float time)
        {
            yield return new WaitForSeconds(time);
            source.isPlaying = false;
        }


        private const string Mixer_Music = "MusicVolume";
        private const string Mixer_Effect = "EffectVolume";
        public void SetBGMVolume(float value)
        {
            mixer.SetFloat(Mixer_Music, Mathf.Log10(value + 0.01f) * 20f);
        }
        public void SetEffectVolume(float value)
        {
            mixer.SetFloat(Mixer_Effect, Mathf.Log10(value + 0.01f) * 20f);
        }

        public void TestEffectVolume()
        {
            Play("effect","click");
        }
    }
}