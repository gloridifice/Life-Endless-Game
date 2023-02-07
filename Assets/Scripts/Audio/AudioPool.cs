using System;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    public class AudioPool : MonoBehaviour
    {
        public int size = 10;
        private List<AudioSource> disableObjects;
        private List<AudioSource> activeObjects;
        private void Awake()
        {
            disableObjects = new List<AudioSource>();
            activeObjects = new List<AudioSource>();
            for (int i = 0; i < size; i++)
            {
                GameObject item = new GameObject();
                item.transform.parent = transform;
                AudioSource source= item.AddComponent<AudioSource>();
                item.SetActive(false);
                disableObjects.Add(source);
            }
        }

        private void Update()
        {
            for (int i = 0; i < activeObjects.Count; i++)
            {
                AudioSource source = activeObjects[i];
                if (!source.isPlaying)
                {
                    DisableOneSource(source);
                }
            }
        }

        public void PutAudio(AudioClip audioClip, AudioPlayData data)
        {
            AudioSource audioSource = ActiveOneSource();
            audioSource.clip = audioClip;
            audioSource.volume = data.volume;
            audioSource.pitch = data.pitch;
            audioSource.loop = data.loop;
            audioSource.Play();
        }

        private AudioSource ActiveOneSource()
        {
            AudioSource audioSource = disableObjects[0];
            audioSource.gameObject.SetActive(true);
            disableObjects.RemoveAt(0);
            activeObjects.Add(audioSource);
            return audioSource;
        }

        private void DisableOneSource(AudioSource source)
        {
            activeObjects.Remove(source);
            source.gameObject.SetActive(false);
            disableObjects.Add(source);
        }
    }
}