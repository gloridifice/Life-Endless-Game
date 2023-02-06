using System.Collections;
using UnityEngine;

namespace GGJ2023
{
    public abstract class TimedAnim
    {
        public float duration;
        public float delay;
        public abstract void Play();

        public TimedAnim()
        {
        }
        public IEnumerator DelayPlayAnim(float time)
        {
            yield return new WaitForSeconds(time);
            Play();
        }
    }
}