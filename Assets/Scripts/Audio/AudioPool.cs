using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    public class AudioPool : MonoBehaviour
    {
        public int size = 24;
        private Stack<AudioController> pool;
        private void Awake()
        {
            pool = new Stack<AudioController>();
            for (int i = 0; i < size; i++)
            {
                Push(CreateNewSource());
            }
        }
        
        public AudioController PutAudio(AudioClip audioClip, AudioPlayData data)
        {
            AudioController controller = Pop();
            controller.gameObject.SetActive(true);
            controller.Play(audioClip, data);
            return controller;
        }

        public void Push(AudioController controller)
        {
            controller.gameObject.SetActive(false);
            pool.Push(controller);
        }
        public AudioController Pop()
        {
            if (pool.TryPop(out AudioController source))
            {
                return source;
            }
            else
            {
                Push(CreateNewSource());
                return Pop();
            }
        }

        public AudioController CreateNewSource()
        {
            GameObject item = new GameObject();
            item.transform.parent = transform;
            AudioSource source= item.AddComponent<AudioSource>();
            AudioController controller= item.AddComponent<AudioController>();
            controller.Init(source);
            item.SetActive(false);
            return controller;
        }
    }
}