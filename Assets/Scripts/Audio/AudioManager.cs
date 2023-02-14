using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using GGJ2023.Level;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GGJ2023.Audio
{
    [RequireComponent(typeof(AudioPool))]
    public class AudioManager : MonoBehaviour
    {
        [HideInInspector] public AudioPool audioPool;
        public AudioRootDatabaseObject rootDatabase;
        public AudioMixer mixer;


        public static AudioManager Instance { get; private set; }
        
        
        private static readonly SoundSource SpringBgmSoundSource = new ("bgm", "spring", new AudioPlayData.Builder().Loop(true).Fade(true).StopOnSceneChanged(false));
        private static readonly SoundSource SummerBgmSoundSource = new("bgm", "summer",new AudioPlayData.Builder().Loop(true).Fade(true).StopOnSceneChanged(false));
        private static readonly SoundSource AutumnBgmSoundSource = new("bgm", "autumn",new AudioPlayData.Builder().Loop(true).Fade(true).StopOnSceneChanged(false));
        private static readonly SoundSource WinterBgmSoundSource = new("bgm", "winter",new AudioPlayData.Builder().Loop(true).Fade(true).StopOnSceneChanged(false));
        readonly SoundSource[] bgmSources = { SpringBgmSoundSource, SummerBgmSoundSource, AutumnBgmSoundSource, WinterBgmSoundSource };
        
        private void Update()
        {
            PlayBGM();
        }

        public void PlayBGM()
        {
            if (GameManager.Instance.isInLevel)
            {
                int levelIndex = GameManager.Instance.currentLevelIndex;
                
                if (levelIndex >= 1 && levelIndex <= 6)
                {
                    PlaySeasonSource(SummerBgmSoundSource);
                }
                else if (levelIndex >= 7 && levelIndex <= 13)
                {
                    PlaySeasonSource(AutumnBgmSoundSource);
                }
                else if (levelIndex >= 14 && levelIndex <= 18)
                {
                    PlaySeasonSource(WinterBgmSoundSource);
                }
                else if (levelIndex >= 19 && levelIndex <= 24)
                {
                    PlaySeasonSource(SpringBgmSoundSource);
                }
            }
            else
            {
                PlaySeasonSource(AutumnBgmSoundSource);
            }
        }

        void PlaySeasonSource(SoundSource seasonSource)
        {
            if (!seasonSource.isPlaying)
            {
                seasonSource.Play();
                List<SoundSource> list = new List<SoundSource>(bgmSources);
                list.Remove(seasonSource);
                foreach (var source in list)
                {
                    source.Stop();
                }
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
                Destroy(gameObject);
            }
            rootDatabase.Init();
            audioPool = GetComponent<AudioPool>();
        }

        public AudioController Play(string dbName, string aName, AudioPlayData.Builder playDataBuilder)
        {
            AudioDatabaseObject database = rootDatabase.GetDatabase(dbName);
            AudioGroupData groupData = database.GetGroup(aName);
            AudioData audioData = groupData.GetNextAudioData();
            float volume = database.volume * groupData.groupVolume * audioData.volume;
            float pitch = database.pitch * groupData.groupPitch * audioData.pitch;
            bool loop = groupData.groupLoop;
            AudioMixerGroup group = database.mixerGroup;

            playDataBuilder = playDataBuilder ?? new AudioPlayData.Builder();
            AudioPlayData playData = playDataBuilder.Volume(volume).Pitch(pitch).Loop(loop).MixerGroup(group).build();
            return audioPool.PutAudio(audioData.audioClip, playData);
        }
        public AudioController Play(string dbName, string aName, bool fade = false)
        {
            return Play(dbName, aName, new AudioPlayData.Builder().Fade(fade));
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