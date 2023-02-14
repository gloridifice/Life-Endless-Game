using System;
using DG.Tweening;
using GGJ2023.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJ2023.UI
{
    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Instance { get; private set; }

        Camera camera;
        private Vector3 lastMousePos = Vector3.zero;
        public bool controlAble = true;
        public float scrollSpeed;
        public float minCamPos = -1.4f;
        public float maxCamPos = 22.5f;
        public float drag = 0.1f;
        private float camVelocity;
        private Vector3 defaultCamPos;

        public CanvasGroup infoPanel;

        public bool isInfoOpen;
        public bool isSettingsOpen;

        public SoundSource clickSoundSource = new SoundSource("effect","click");

        private void Awake()
        {
            Instance = this;
            camera = Camera.main;
            defaultCamPos = camera.transform.localPosition;
            infoPanel.alpha = 0;
        }

        private void Update()
        {
            
            if (controlAble && EventSystem.current.currentSelectedGameObject == null)
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 delta = Input.mousePosition - lastMousePos;
                    float clamped = delta.x;
                    if (Mathf.Abs(clamped) > 1)
                    {
                        clamped = Mathf.Sign(clamped) * Mathf.Clamp(Mathf.Abs(clamped), 12, 20);
                        clamped = Mathf.Sign(clamped) * (Mathf.Abs(clamped) + 16);
                    }
                    else
                    {
                        clamped = 0;
                    }
                    float d = -clamped / 100f * scrollSpeed;
                    //camVelocity = d; //Mathf.Sign(d) * Mathf.Max(Mathf.Abs(d), Mathf.Abs(camVelocity));
                    if (Math.Sign(d) == Math.Sign(camVelocity))
                    {
                        camVelocity = Mathf.Sign(d) * Mathf.Max(Mathf.Abs(d), Mathf.Abs(camVelocity));
                    }
                    else
                    {
                        camVelocity = d;
                    }

                }
            }
            lastMousePos = Input.mousePosition;

            float drag = Mathf.Abs(camVelocity) * this.drag;
            camVelocity = Mathf.Sign(camVelocity) * Mathf.Max( Mathf.Abs(camVelocity) - drag * Time.deltaTime * 50f, 0);
            MoveCamera(camVelocity * Time.deltaTime * 50f);

            Vector3 pos = camera.transform.localPosition;
            pos.z = defaultCamPos.z + Mathf.Sin(Time.time / 2) * 0.3f;
            camera.transform.localPosition = pos;
        }

        void MoveCamera(float d)
        {
            Vector3 pos = camera.transform.position;
            pos.x = Mathf.Clamp(pos.x + d, minCamPos, maxCamPos);
            camera.transform.position = pos;
        }

        public void SwitchInfo()
        {
            if (isInfoOpen)
            {
                infoPanel.DOFade(0f, 1f);
            }
            else
            {
                infoPanel.DOFade(1f,1f);
            }
            isInfoOpen = !isInfoOpen;
        }
        

        public void StartGame()
        {
            GameManager.Instance.LoadLevel(1);
        }

        public void ExitGame()
        {
            GameManager.Instance.ExitGame();
        }

        public void ChangeVolume(float value)
        {
            AudioListener.volume = value;
        }

        public void SwitchSettingsPanel()
        {
            GameManager.Instance.SwitchSettingsPanel();
        }
    }
}