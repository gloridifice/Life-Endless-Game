using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using GGJ2023.TileObject;
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
        public static bool assetsLoaded = false;
        public static Dictionary<string, RegistryTileObject> registryTileObjects;
        public Action onAssetsLoaded = () => {};

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
                LoadAssets();
            }
        }

        private void Update()
        {
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

        public void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private async void LoadAssets()
        {
            FieldInfo[] infos = typeof(TileObjectsReferences).GetFields();
            foreach (var info in infos)
            {
                object value = info.GetValue(null);
                if (value is RegistryTileObject registry)
                {
                    registry.LoadPrefab();
                    if(!registryTileObjects.ContainsKey(registry.registryName)) registryTileObjects.Add(registry.registryName, registry);
                }
            }
            witheredSpriteMaterial = await witheredSprMatReference.LoadAssetAsync<Material>().Task;
            assetsLoaded = true;
        }

        public void LoadLevel(int level)
        {
            string levelIndex = String.Format("{0,18:000}", level);
            string levelIndex1= levelIndex.Replace( " ", "" ); 
            SceneManager.LoadScene("S" + levelIndex1);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}