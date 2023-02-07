using GGJ2023.Level;
using UnityEngine;
using GGJ2023.Audio;
using System.Collections.Generic;

namespace GGJ2023.TileObject
{
    public class FireBlockerTree : TreeTileObject
    {
        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            
            foreach (TileObject tileObject in others)
            {
                if (tileObject.Is(TileObjectsReferences.excavatorForward)||
                    (tileObject.Is(TileObjectsReferences.fire) && !IsMature))
                {
                    Die(levelManager);
                }
            }
        }
        
        public override void ExecuteTreeEffect(LevelManager levelManager)
        {
            //如果检测到周围有火触发挡火动画，否则切换为默认动画
            if (levelManager.Neighbour4TilesHasType(CellPos, TileObjectsReferences.fire))
            {
                //animator.SetTrigger("blockfire");
                //AudioManager.Instance.Play("effect", "fireblockertree");
                AnimationEvent.canPlayfireBlockerTreeAudio = true;
                animator.SetBool("blockfire", true);
            }
            else
            {
                //animator.SetTrigger("Idle");
                animator.SetBool("blockfire", false);
            }
        }
    }
}