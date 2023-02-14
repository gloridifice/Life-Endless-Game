using DG.Tweening;
using GGJ2023.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2023.UI
{
    public class SettingsUI : MonoBehaviour
    {
        public CanvasGroup uiCanvasGroup;
        public RectTransform returnToTitleButton;
        public bool ableReturnToTitleButton;
        public Slider bgmVolumeSlider;
        public Button effectTestButton;
        public Slider effectVolumeSlider;
        public bool isOpen;
        public float fadeInDuration = 1f;
        public float fadeOutDuration = 1f;

        public void Init()
        {
            uiCanvasGroup.alpha = 0;
            uiCanvasGroup.gameObject.SetActive(false);

            bgmVolumeSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetBGMVolume(value));
            effectVolumeSlider.onValueChanged.AddListener((value) => AudioManager.Instance.SetEffectVolume(value));
            effectTestButton.onClick.AddListener(() => AudioManager.Instance.TestEffectVolume());
        }
        public void SwitchUI()
        {
            CloseUI();
            OpenUI();
        }

        public void CloseUI()
        {
            if (isOpen)
            {
                uiCanvasGroup.interactable = false;
                Tweener tweener = uiCanvasGroup.DOFade(0, fadeOutDuration);
                tweener.OnKill(() =>
                {
                    isOpen = false;
                    uiCanvasGroup.gameObject.SetActive(false);
                });
            }
        }

        public void OpenUI()
        {
            if (!isOpen){
                uiCanvasGroup.gameObject.SetActive(true);
                returnToTitleButton.gameObject.SetActive(ableReturnToTitleButton);
                Tweener tweener = uiCanvasGroup.DOFade(1, fadeInDuration);
                tweener.OnKill(() =>
                {
                    uiCanvasGroup.interactable = true;
                    isOpen = true;
                });
            }
        }
    }
}