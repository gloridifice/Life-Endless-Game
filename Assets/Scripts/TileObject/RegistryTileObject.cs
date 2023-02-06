using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2023.TileObject
{
    public class RegistryTileObject
    {
        public GameObject prefab;
        public AssetReference assetReference;
        public string registryName;

        public RegistryTileObject(string registryName, string assetsPath)
        {
            if (assetsPath != null)
            {
                assetReference = new AssetReference(assetsPath);
            }

            this.registryName = registryName;
        }

        public async void LoadPrefab()
        {
            if (assetReference == null) return;
            if (prefab != null) return;
            prefab = await assetReference.LoadAssetAsync<GameObject>().Task;
            if (prefab.TryGetComponent(out TileObject tileObject))
            {
                tileObject.registryName = registryName;
            }
        }
    }
}