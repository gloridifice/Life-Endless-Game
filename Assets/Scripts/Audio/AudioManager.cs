using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJ2023.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [Range(0,1)]
        public float bgmVolume;
        [Range(0,1)]
        public float effectVolume;
        
        [Header("BGM")]
        public AudioSource bgmSource;
        public List<AudioClip> bgmClips;
        
        [Header("Wind")]
        public AudioSource windSource;
        public List<AudioClip> windClips;
        
        

        public static AudioManager instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            if (instance != this)
            {
                Destroy(this);
            }

            bgmSource.volume = bgmVolume;
            windSource.volume = effectVolume - 0.1f;
        }

        private void Update()
        {
            if (!bgmSource.isPlaying)
            {
                bgmSource.clip = bgmClips[Random.Range(0, bgmClips.Count)];
                bgmSource.Play();
            }
        }

        public void PlayWindSound()
        {
            if (!windSource.isPlaying)
            {
                windSource.clip = windClips[Random.Range(0, windClips.Count)];
                windSource.Play();
            }
        }
    }
}