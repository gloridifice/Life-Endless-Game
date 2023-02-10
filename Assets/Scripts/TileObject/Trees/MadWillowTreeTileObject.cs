using System.Collections;
using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class MadWillowTreeTileObject : TreeTileObject
    {
        public GameObject effectPrefab;
        public override void ExecuteTreeEffect(LevelManager levelManager)
        {
            if (levelManager.HasAndGetNeighbour8Tiles(CellPos, out List<TileObject[]> list))
            {
                foreach (var tiles in list)
                {
                    foreach (var tile in tiles)
                    {
                        //如果是动物或挖掘机就死亡,并播放击打动画
                        if (tile.Is(TileObjectsReferences.animalBack)|| tile.Is(TileObjectsReferences.excavatorBack))
                        {
                            levelManager.AddAnimation(new AnimatorTimedAnim(animator, "attack"));
                            StartCoroutine(WaitForSmoke(0.5f,tile.Tilemap.layoutGrid.CellToWorld(tile.CellPos)));
                            
                            if (tile != null)
                            {
                                tile.Die(LevelManager, 0.6f);
                            }
                        }
                    }
                }
            }
        }

        IEnumerator WaitForSmoke(float time, Vector3 pos)
        {
            yield return new WaitForSeconds(time);
            Instantiate(effectPrefab, pos, new Quaternion());
        }
    }
}