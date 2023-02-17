using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class ExtinguishingTreeTileObject : TreeTileObject
    {
        public override void ExecuteTreeEffect(LevelManager levelManager)
        {
            if (levelManager.HasAndGetNeighbour8Tiles(CellPos, out List<TileObject[]> list))
            {
                foreach (var tiles in list)
                {
                    foreach (var tile in tiles)
                    {
                        //如果是火焰就死亡，并播放灭火动画

                        if (tile.Is(TileObjectsReferences.fire))
                        {
                            levelManager.AddAnimation(new AnimatorTimedAnim(animator, "extinguish"));
                            StartCoroutine(WaitExtinguishStop(levelManager));
                            //tile.Die(levelManager);
                        }
                    }
                }
            }
        }

        IEnumerator<int> WaitExtinguishStop(LevelManager levelManager)
        {
            yield return 0;
            AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

            //灭火动画结束后，才摧毁物体
            if (stateinfo.IsName("extinguish") && (stateinfo.normalizedTime > 0.8f))
            {
                if (levelManager.HasAndGetNeighbour8Tiles(CellPos, out List<TileObject[]> list))
                {
                    foreach (var tiles in list)
                    {
                        foreach (var tile in tiles)
                        {
                            //如果是火焰就摧毁
                            if (tile.Is(TileObjectsReferences.fire))
                            {
                                tile.Die(levelManager);
                            }
                        }
                    }
                }
            }
            else
            {
                StartCoroutine(WaitExtinguishStop(levelManager));
            }
        }
    }
}