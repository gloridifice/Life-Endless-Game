using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    public class AudioPool : MonoBehaviour
    {
        public int size = 24;
        private Stack<AudioSource> pool;
        private void Awake()
        {
            pool = new Stack<AudioSource>();
            for (int i = 0; i < size; i++)
            {
                Push(CreateNewSource());
            }
        }
        
        public void PutAudio(AudioClip audioClip, AudioPlayData data)
        {
            AudioSource audioSource = Pop();
            audioSource.gameObject.SetActive(true);
            audioSource.clip = audioClip;
            audioSource.volume = data.volume;
            audioSource.pitch = data.pitch;
            audioSource.loop = data.loop;
            audioSource.Play();
            StartCoroutine(WaitToPush(audioClip.length, audioSource));
        }

        public IEnumerator WaitToPush(float time, AudioSource source)
        {
            yield return new WaitForSeconds(time);
            source.gameObject.SetActive(false);
            Push(source);
        }

        public void Push(AudioSource source)
        {
            pool.Push(source);
        }
        public AudioSource Pop()
        {
            if (pool.TryPop(out AudioSource source))
            {
                return source;
            }
            else
            {
                Push(CreateNewSource());
                return Pop();
            }
        }

        public AudioSource CreateNewSource()
        {
            GameObject item = new GameObject();
            item.transform.parent = transform;
            AudioSource source= item.AddComponent<AudioSource>();
            source.playOnAwake = false;
            item.SetActive(false);
            return source;
        }
    }
}