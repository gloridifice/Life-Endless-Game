using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GGJ2023.UI
{
    public class ColliderButton : MonoBehaviour
    {
        public RectTransform imageTrans;
        public AnimationCurve curve;
        public float maxOffset;
        public UnityEvent onClick;
        private float time;
        public float duration = 0.3f;
        public bool mouseOver;
        private Vector2 defaultPos;

        private void Awake()
        {
            defaultPos = imageTrans.anchoredPosition;
        }

        public void OnMouseDown()
        {
            onClick.Invoke();
        }

        public void OnMouseEnter()
        {
            mouseOver = true;
        }

        private void OnMouseExit()
        {
            mouseOver = false;
        }

        private void Update()
        {
            if (mouseOver) time += Time.deltaTime;
            else time -= Time.deltaTime;

            time = Mathf.Clamp(time, 0, duration);


            imageTrans.anchoredPosition = defaultPos + new Vector2( curve.Evaluate(time / duration) * maxOffset,0) ;
            
        }
    }
}