using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GGJ2023.Level;
using GGJ2023.TileObject;
using GGJ2023.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace GGJ2023
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }
        public static GameManager instance;
        public bool assetsLoaded = false;
        public static Dictionary<string, RegistryTileObject> registryTileObjects;
        public Action onAssetsLoaded = () => {};
        public List<AssetReference> assetsToLoad;

        [HideInInspector]
        public Material witheredSpriteMaterial;

        private AssetReference witheredSprMatReference =
            new AssetReference("Assets/Materials/SpritesMaterials/m_Sprites_Withered.mat");
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            if (Instance != this)
            {
                Destroy(this);
            }
            
            if (registryTileObjects == null)
            {
                registryTileObjects = new Dictionary<string, RegistryTileObject>();
            }
            if (!assetsLoaded)
            {
                assetsToLoad = new List<AssetReference>();
                LoadTileObjectAssets();
            }

            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        public void OnSceneChanged(Scene last, Scene current)
        {
            TryInitLevel();
        }

        public void TryInitLevel()
        {
            if (LevelManager.Instance != null && assetsLoaded)
            {
                LevelManager.Instance.InitLevel();
                Debug.Log("Level Init!");
            }
        }

        #region Assets
        public bool IsAssetsLoaded()
        {
            if (assetsToLoad == null)
            {
                return false;
            }
            foreach (var asset in assetsToLoad)
            {
                if (!asset.IsDone)
                {
                    return false;
                }
            }
            foreach (var pair in registryTileObjects)
            {
                if (pair.Value.prefab == null)
                {
                    return false;
                }
            }
            return true;
        }
        
        private void OnAssetsLoaded()
        {
            TryInitLevel();
            Debug.Log("Assets Loaded!");
        }
        private async void LoadTileObjectAssets()
        {
            FieldInfo[] infos = typeof(TileObjectsReferences).GetFields();
            foreach (var info in infos)
            {
                object value = info.GetValue(null);
                if (value is RegistryTileObject registry)
                {
                    assetsToLoad.Add(registry.assetReference);
                    registry.LoadPrefab();
                    if(!registryTileObjects.ContainsKey(registry.registryName)) registryTileObjects.Add(registry.registryName, registry);
                }
            }
            witheredSpriteMaterial = await witheredSprMatReference.LoadAssetAsync<Material>().Task;
            assetsToLoad.Add(witheredSprMatReference);
        }
        
        #endregion

        private void Update()
        {
            if (!assetsLoaded)
            {
                if (IsAssetsLoaded())
                {
                    assetsLoaded = true;
                    OnAssetsLoaded();
                }
            }

            //todo 退出到主菜单
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadMainMenu();
            }
            //重开关卡
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadCurrentScene();
            }
        }


        #region Transiton & Level

        public GameObject transitionPrefab;

        public void LoadScene(string sceneName)
        {
            GameObject transObj = Instantiate(transitionPrefab);
            if (transObj.TryGetComponent(out Transition transition))
            {
                transition.TransitionOut();
                transition.onTransitionOutComplete += () =>
                {
                    SceneManager.LoadScene(sceneName);
                };
            }
        }
        public void ReloadCurrentScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadLevel(int level)
        {
            string levelIndex = String.Format("{0,18:000}", level);
            string levelIndex1= levelIndex.Replace( " ", "" );
            LoadScene("S" + levelIndex1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        #endregion
    }
}