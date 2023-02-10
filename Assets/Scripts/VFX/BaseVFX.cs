using System;
using UnityEngine;
using UnityEngine.VFX;

namespace GGJ2023.VFX
{
    public class BaseVFX : MonoBehaviour
    {
        public float duration;
        protected VisualEffect effect;
        protected bool stopped;

        protected void Awake()
        {
            Init();
            Invoke(nameof(Stop), duration);
        }

        protected virtual void Init()
        {
            effect = GetComponentInChildren<VisualEffect>();
        }
        private void Update()
        {
            if (ShouldDie())
            {
                Destroy(gameObject);
            }
        }

        public virtual bool ShouldDie()
        {
            return stopped && effect.aliveParticleCount == 0;
        }
        public virtual void Stop()
        {
            effect.Stop();
            stopped = true;
        }
    }
}