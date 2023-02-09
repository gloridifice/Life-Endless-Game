using UnityEngine;

namespace GGJ2023.VFX
{
    public class ParticleVFX : BaseVFX
    {
        private ParticleSystem particle;
        protected override void Init()
        {
            particle = GetComponentInChildren<ParticleSystem>();
            this.duration = particle.main.duration;
        }

        public override bool ShouldDie()
        {
            return stopped && particle.particleCount == 0;
        }

        public override void Stop()
        {
            stopped = true;
        }
    }
}