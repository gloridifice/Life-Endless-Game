using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2023
{
    [CreateAssetMenu(fileName = "RandomGameObjectTile", menuName = "Custom Rule Tile/Random")]
    public class RandomGameObjectTile : TileBase
    {
        public Sprite sprite;
        public GameObject[] gameObjects;
        public bool isRandomRotate;
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            var iden = Matrix4x4.identity;
            base.GetTileData(position, tilemap, ref tileData);
            tileData.sprite = sprite;
            tileData.gameObject = gameObjects[(int)(Random.Range(0, 1000)) % gameObjects.Length];
            Matrix4x4 matrix4X4 = iden;
            if (isRandomRotate)
            {
                float angle = (int)(Random.Range(0, 4)) * 90f;
                matrix4X4 = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -angle), Vector3.one);
            }
            tileData.transform = matrix4X4;
        }
        
    }
}