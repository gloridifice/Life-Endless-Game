using System;
using UnityEngine;
using UnityEngine.VFX;

namespace GGJ2023.VFX
{
    public class BaseVFX : MonoBehaviour
    {
        public float duration;
        private VisualEffect effect;
        private bool stopped;

        private void Awake()
        {
            effect = GetComponentInChildren<VisualEffect>();
            Invoke(nameof(Stop), duration);
        }

        private void Update()
        {
            if (stopped && effect.aliveParticleCount == 0)
            {
                Destroy(gameObject);
            }
        }

        public void Stop()
        {
            effect.Stop();
            stopped = true;
        }
    }
}