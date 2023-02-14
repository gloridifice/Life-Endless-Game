using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2023.Audio
{
    public class AudioController : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioPlayData playData;
        public bool fade;
        public bool IsPlaying => audioSource.isPlaying;

        public float lifeCountDown;

        public Action onDie;

        public void Init(AudioSource source)
        {
            this.audioSource = source;
            this.audioSource.playOnAwake = false;
            onDie = () => { };
        }

        private void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= OnSceneChanged;
        }

        private void Update()
        {
            if (lifeCountDown > 0)
            {
                lifeCountDown = Mathf.Max(lifeCountDown - Time.deltaTime, 0);
            }
            if (lifeCountDown == 0)
            {
                Stop();
            }
        }

        private void OnSceneChanged(Scene old, Scene newScene)
        {
            if (playData.stopOnSceneChange && IsPlaying)
            {
                fade = true;
                Stop();
            }
        }
        
        public void Play(AudioClip clip, AudioPlayData data)
        {
            this.playData = data;
            audioSource.clip = clip;
            audioSource.volume = data.volume;
            audioSource.pitch = data.pitch;
            audioSource.loop = data.loop;
            audioSource.outputAudioMixerGroup = data.mixerGroup;
            this.fade = data.fade;
            this.lifeCountDown = clip.length;

            if (fade)
            {
                audioSource.volume = 0;
                audioSource.DOFade(data.volume, 1f);
            }
            audioSource.Play();

        }

        public void Stop()
        {
            lifeCountDown = -1;
            if (fade)
            {
                Tweener tweener = audioSource.DOFade(0, 1.5f);
                tweener.onKill += Die;
            }
            else
            {
                Die();
            }
        }

        private void Die()
        {
            onDie.Invoke();
            this.gameObject.SetActive(false);
            AudioManager.Instance.audioPool.Push(this);
        }
    }
}