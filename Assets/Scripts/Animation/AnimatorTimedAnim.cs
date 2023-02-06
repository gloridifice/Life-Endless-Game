using System.Collections;
using UnityEngine;

namespace GGJ2023
{
    public class AnimatorTimedAnim : TimedAnim
    {
        private Animator animator;
        private string animName;
        public AnimatorTimedAnim(Animator animator, string animName, float delay = 0)
        {
            this.delay = delay;
            this.animator = animator;
            this.animName = animName;
            duration = GetAnimatorLength(animator, animName) + delay;
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
            animator.Play(animName);
            //AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            //duration = info.length + delay;
            //duration = GetAnimatorLength(animator, animName) + delay;
            //Debug.Log(duration);
        }

        /// <summary>
        /// ���animator��ĳ������Ƭ�ε�ʱ������
        /// </summary>
        public float GetAnimatorLength(Animator animator, string name)
        {
            //����Ƭ��ʱ�䳤��
            float length = 0;

            AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

            foreach (AnimationClip clip in clips)
            {
                if (clip.name.Equals(name))
                {
                    length = clip.length;
                    break;
                }
            }
            return length;
        }

    }
}