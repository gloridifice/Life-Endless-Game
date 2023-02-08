using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class ExcavatorTileObject : DirectionalTileObject
    {
        public override void OnRoundExecute(LevelManager levelManager)
        {
            if (!CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                moveDirection = DirectionTypeUtils.GetOpposite(moveDirection);
            }

            if (CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                animator.SetTrigger("move");
                levelManager.AddAnimation(new AnimatorTimedAnim(animator, "move"));
                Move(DirectionTypeUtils.GetVectorFromDirection(moveDirection));
                //Move(DirectionTypeUtils.GetVectorFromDirection(moveDirection));
            }
        }

        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            foreach (TileObject collideTile in others)
            {
                if (collideTile.Is(TileObjectsReferences.fire))
                {
                    Die(levelManager);
                }
                else if(collideTile.Is(TileObjectsReferences.madWillow)|| 
                    collideTile.Is(TileObjectsReferences.extinguishingTree)|| 
                    collideTile.Is(TileObjectsReferences.fireBlockerTree)||
                    collideTile.Is(TileObjectsReferences.tumbleweed))
                {
                    levelManager.AddAnimation(new AnimatorTimedAnim(animator, "attack"));
                }
            }
        }

        public override void OnRoundEnd(LevelManager levelManager)
        {
            if (!CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                moveDirection = DirectionTypeUtils.GetOpposite(moveDirection);
            }
        }

        IEnumerator<int> WaitGatherStop(LevelManager levelManager)
        {
            yield return 0;
            if (animator != null)
            {
                AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

                //蓄力动画结束后，可以移动
                if (stateinfo.IsName("gather") && (stateinfo.normalizedTime > 0.9f))
                {
                    Move(DirectionTypeUtils.GetVectorFromDirection(moveDirection));
                }
                else
                {
                    StartCoroutine(WaitGatherStop(levelManager));
                }
            }
        }


    }
}