using System;
using DG.Tweening;
using GGJ2023.Level;
using GGJ2023.TileObject;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ2023.UI
{
    public class LevelUIManager : MonoBehaviour
    {
        [HideInInspector] public LevelManager levelManager;
        [Header("Seeds")] public CanvasGroup seedPanel;
        public int seedFadeRound = 2; //渐变至半透明的回合
        public TMP_Text extinguishingTreeAmount;
        public TMP_Text fireBlockerTreeAmount;
        public TMP_Text madWillowAmount;

        [Header("Key Map")] public CanvasGroup keyMapPanel;
        public bool activeKeyMap = false;
        public int keyMapFadeOutRound = 2;
        public float keyMapFadeOutTime = 2f;

        [Header("Enter Text")] public TMP_Text enterTMPText;
        public float fadeInTime = 1f;
        public float fadeOutTime = 0.5f;

        [Header("Flower")] 
        public TMP_Text flowerAmountText;

        public RectTransform flowerGradient;
        
        [Header("Win")]
        public CanvasGroup winPanel;
        public RectTransform nextLevelButton;

        [Header("Thanks")] 
        public RectTransform thanksPanel;

        private void Update()
        {
            
        }

        public void InitUI(LevelManager levelManager)
        {
            this.levelManager = levelManager;
            
            //Win Panel
            winPanel.gameObject.SetActive(false);
            
            //Seed
            UpdateSeedAmountUI();

            //Enter Text
            enterTMPText.gameObject.SetActive(true);
            enterTMPText.text = levelManager.enterText;
            FadeInEnterText();

            //Key Map
            keyMapPanel.gameObject.SetActive(activeKeyMap);
            
            //Flower
            OnFlowerAmountChanged(levelManager.flowerCount);
            
            thanksPanel.gameObject.SetActive(false);
        }
        public void OnWin()
        {
            winPanel.gameObject.SetActive(true);
            winPanel.alpha = 0;
            Sequence sequence = DOTween.Sequence();
            Tweener tweener = flowerGradient.DOScaleY(1.5f, 0.6f);
            tweener.SetEase(Ease.InOutQuart);
            sequence.Append(tweener);
            sequence.Insert(0.3f,winPanel.DOFade(1, 0.8f));

            string index = SceneManager.GetActiveScene().name.Replace("S", "");
            int a = int.Parse(index);
            if (a == 24)
            {
                thanksPanel.gameObject.SetActive(true);
                nextLevelButton.gameObject.SetActive(false);
            }
        }

        public void NextLevel()
        {
            string index = SceneManager.GetActiveScene().name.Replace("S", "");
            int a = int.Parse(index);
            GameManager.Instance.LoadLevel(a + 1);
        }

        public void ReturnToTile()
        {
            GameManager.Instance.LoadMainMenu();
        }
        #region Flower

        public void OnFlowerAmountChanged(int amount)
        {
            flowerAmountText.text = amount + "/" + levelManager.victoryFlowerAmount;
        }

        #endregion

        #region Input

        public void HandleInput()
        {
            if (Input.anyKeyDown)
            {
                FadeOutEnterText();
                if (levelManager.roundCount == seedFadeRound)
                {
                    FadeOutSeed();
                }

                if (activeKeyMap && levelManager.roundCount == keyMapFadeOutRound)
                {
                    FadeOutKeyMap();
                }
            }
        }

        #endregion

        public void FadeOutKeyMap()
        {
            if (activeKeyMap)
            {
                Tweener tweener = keyMapPanel.DOFade(0.5f, keyMapFadeOutTime);
                //tweener.onComplete += () => { keyMapPanel.gameObject.SetActive(false); };
            }
        }

        #region Seed

        public void SetSeedAmount(RegistryTileObject treeType, int amount)
        {
            if (treeType == TileObjectsReferences.extinguishingTree)
            {
                extinguishingTreeAmount.text = amount.ToString();
            }

            if (treeType == TileObjectsReferences.fireBlockerTree)
            {
                fireBlockerTreeAmount.text = amount.ToString();
            }

            if (treeType == TileObjectsReferences.madWillow)
            {
                madWillowAmount.text = amount.ToString();
            }
        }

        public void UpdateSeedAmountUI()
        {
            SetSeedAmount(TileObjectsReferences.extinguishingTree,
                levelManager.seedsCounts[TileObjectsReferences.extinguishingTree]);
            SetSeedAmount(TileObjectsReferences.fireBlockerTree,
                levelManager.seedsCounts[TileObjectsReferences.fireBlockerTree]);
            SetSeedAmount(TileObjectsReferences.madWillow, levelManager.seedsCounts[TileObjectsReferences.madWillow]);
        }

        public void OnSeedChanged()
        {
            UpdateSeedAmountUI();
            seedPanel.alpha = 1;
            Tweener tweener = seedPanel.DOFade(0.5f, 2f);
        }

        public void FadeOutSeed()
        {
            Tweener tweener = seedPanel.DOFade(0.5f, 1f);
        }

        #endregion

        #region EnterText

        public void FadeOutEnterText()
        {
            Tweener tweener = enterTMPText.DOFade(0, fadeOutTime);
            tweener.onComplete += () => { enterTMPText.gameObject.SetActive(false); };
        }

        public void FadeInEnterText()
        {
            enterTMPText.alpha = 0;
            Tweener tweener = enterTMPText.DOFade(1, fadeInTime);
        }

        #endregion


    }
}