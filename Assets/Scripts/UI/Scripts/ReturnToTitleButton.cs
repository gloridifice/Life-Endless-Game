using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJ2023.UI
{
    public class ReturnToTitleButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        private float defaultX;
        private RectTransform rectTransform;
        private float time;
        public float duration;
        private bool animating;
        public float offset;
        public AnimationCurve curve;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultX = rectTransform.anchoredPosition.x;
        }

        private void Update()
        {
            if (animating)
            {
                time += Time.deltaTime;
            }
            else
            {
                time -= Time.deltaTime;
            }

            time = Mathf.Clamp(time, 0, duration);
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x = defaultX - curve.Evaluate(time / duration) * offset;
            rectTransform.anchoredPosition = pos;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            animating = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            animating = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GameManager.Instance.LoadMainMenu();
        }
    }
}