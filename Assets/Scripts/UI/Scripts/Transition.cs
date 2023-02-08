using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.UI
{
    public class Transition : MonoBehaviour
    {
        public Image image;
        public CanvasGroup textGroup;
        public TMP_Text indexTMPText;
        public TMP_Text nameTMPText;
        public Action onTransitionOutComplete = () => {};
        public Action onTransitionInComplete = () => {};
        public bool info = true;

        private void Awake()
        {
            textGroup.alpha = 0f;
            image.material.SetFloat("_StepIn",1f);
        }

        public void SetLevelInfo(int levelIndex, string levelName)
        {
            indexTMPText.text = levelIndex.ToString();
            nameTMPText.text = levelName;
        }
        /// <summary>
        /// 进入场景时触发
        /// </summary>
        public void TransitionIn()
        {
            Material material = image.material;

            textGroup.alpha = 0f;
            material.SetFloat("_StepIn",1f);
            
            Tweener textFadeTwn0 = textGroup.DOFade(1,0.6f);
            Tweener textFadeTwn1 = textGroup.DOFade(1,1f);
            Tweener textFadeTwn2 = textGroup.DOFade(0,0.5f);
            Tweener imageStepTwn = material.DOFloat(0, "_StepIn", 0.5f);
            imageStepTwn.SetEase(Ease.OutQuart);
            
            Sequence sequence = DOTween.Sequence();
            if (info)
            {
                sequence.Append(textFadeTwn0);
                sequence.Append(textFadeTwn1);
                sequence.Append(textFadeTwn2);
            }
            sequence.Append(imageStepTwn);

            sequence.onKill += () =>
            {
                onTransitionInComplete.Invoke();
                Destroy(this.gameObject);
            };
        }
        /// <summary>
        /// 离开场景时触发
        /// </summary>
        public void TransitionOut()
        {
            Material material = image.material;
            
            textGroup.alpha = 0f;
            material.SetFloat("_StepIn",0f);
            
            Tweener tweener = material.DOFloat(1, "_StepIn", 0.5f);
            tweener.SetEase(Ease.InQuart);

            tweener.onKill += () => { onTransitionOutComplete.Invoke(); };
        }
    }
}