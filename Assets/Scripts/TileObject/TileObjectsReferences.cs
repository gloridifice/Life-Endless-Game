using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GGJ2023.TileObject
{
    public class TileObjectsReferences : MonoBehaviour
    {

        public static RegistryTileObject fire = new("fire", "Assets/Tilemap/Object/Fire/p_Fire.prefab");
        public static RegistryTileObject tumbleweed = new("tumbleweed", "Assets/Tilemap/Object/Tumbleweed/p_Tumbleweed.prefab");

        public static RegistryTileObject seed0 = new("seed", "Assets/Tilemap/Object/Seed/p_Seed_0.prefab");
        public static RegistryTileObject seed1 = new("seed", "Assets/Tilemap/Object/Seed/p_Seed_1.prefab");
        public static RegistryTileObject seed2 = new("seed", "Assets/Tilemap/Object/Seed/p_Seed_2.prefab");

        public static RegistryTileObject wall = new("wall", "Assets/Tilemap/Object/Wall/p_Wall_1.prefab");
        public static RegistryTileObject flower = new("flower", "Assets/Tilemap/Object/Flower/p_Flower.prefab");

        public static RegistryTileObject extinguishingTree = new("extinguishingTree", "Assets/Tilemap/Object/ExtinguishingTree/p_ExtinguishingTree.prefab");
        public static RegistryTileObject fireBlockerTree = new("fireBlockerTree", "Assets/Tilemap/Object/FireBlockerTree/p_FireBlockerTree.prefab");
        public static RegistryTileObject madWillow = new("madWillow", "Assets/Tilemap/Object/MadWillow/p_MadWillow.prefab");

        public static RegistryTileObject animalBack = new("animal", "Assets/Tilemap/Object/Animal/p_Animal_Back.prefab");
        public static RegistryTileObject animalLeft = new("animal", "Assets/Tilemap/Object/Animal/p_Animal_Left.prefab");
        public static RegistryTileObject animalRight = new("animal", "Assets/Tilemap/Object/Animal/p_Animal_Right.prefab");
        public static RegistryTileObject animalForward = new("animal", "Assets/Tilemap/Object/Animal/p_Animal_Forward.prefab");

        public static RegistryTileObject excavatorForward = new("excavator", "Assets/Tilemap/Object/Excavator/p_Excavator_Forward.prefab");
        public static RegistryTileObject excavatorBack = new("excavator", "Assets/Tilemap/Object/Excavator/p_Excavator_Back.prefab");
        public static RegistryTileObject excavatorLeft = new("excavator", "Assets/Tilemap/Object/Excavator/p_Excavator_Left.prefab");
        public static RegistryTileObject excavatorRight = new("excavator", "Assets/Tilemap/Object/Excavator/p_Excavator_Right.prefab");
        
        public static RegistryTileObject GetTileObjectFromName(string registryName)
        {
            return GameManager.registryTileObjects[registryName];
        }
        public static string GetTileRegistryName(GameObject prefab)
        {
            if (prefab.TryGetComponent(out TileObject tileObject))
            {
                return tileObject.registryName;
            }

            return "";
        }

    }
}