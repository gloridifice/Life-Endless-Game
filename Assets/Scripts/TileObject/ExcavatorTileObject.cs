using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class ExcavatorTileObject : DirectionalTileObject
    {
        private void Start()
        {
            if (moveDirection == DirectionType.Right)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (moveDirection == DirectionType.Back)
            {
                transform.GetChild(1).transform.localScale = new Vector3(1, 1, -1);
            }
        }

        public override void OnRoundExecute(LevelManager levelManager)
        {
            if (!CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                moveDirection = DirectionTypeUtils.GetOpposite(moveDirection);
            }

            if (CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                Vector3 scale = transform.localScale;
                Vector3 triangleScale = transform.GetChild(1).transform.localScale;

                if (moveDirection == DirectionType.Right) scale.x = -Mathf.Abs(scale.x);
                if (moveDirection == DirectionType.Left) scale.x = Mathf.Abs(scale.x);
                if (moveDirection == DirectionType.Forward) triangleScale.z = Mathf.Abs(triangleScale.z);
                if (moveDirection == DirectionType.Back) triangleScale.z = -Mathf.Abs(triangleScale.z);
                transform.localScale = scale;
                transform.GetChild(1).transform.localScale = triangleScale;

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

                //????????????????????????
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