using DG.Tweening;

namespace GGJ2023
{
    public class TweenTimedAnim : TimedAnim
    {
        public Tweener tweener;
        public TweenTimedAnim(Tweener tweener, float delay = 0)
        {
            tweener.Pause();
            duration = tweener.Duration() + delay;
            this.delay = delay;
            this.tweener = tweener;
            if (delay > 0)
            {
                GameManager.Instance.StartCoroutine(DelayPlayAnim(delay));
            }
            else
            {
                Play();
            }
        }
        public override void Play()
        {
            tweener.Play();
        }
    }
}