using GGJ2023.Level;
using GGJ2023.MapTile;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class TreeTileObject : TileObject
    {
        public int age = 0;
        public int maxAge = 2;
        private int dynamicMaxAge;

        //public Animator animator;    //动画器
        public bool IsMature => age >= dynamicMaxAge;
        public bool IsJustMature => age == dynamicMaxAge;
        private SpriteRenderer spriteRenderer;
        public SpriteRenderer SpriteRenderer {
            get{
                if (spriteRenderer == null)
                {
                    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                }

                return spriteRenderer;
            }
        }


        public override void Init()
        {
            dynamicMaxAge = maxAge;
            if (LevelManager.HasAndGetMapTile(CellPos, out MapTile.MapTile mapTile))
            {
                if (mapTile.type == MapTileType.Grass)
                {
                    SpriteRenderer.material = GameManager.Instance.witheredSpriteMaterial;
                }
            }
            //transform.localScale = (float)(age + 1) / (float)(maxAge + 1) * Vector3.one;
        }

        public override void OnRoundExecute(LevelManager levelManager)
        {
            if (levelManager.HasAndGetMapTile(CellPos, out MapTile.MapTile mapTile))
            {
                if (mapTile.type == MapTileType.Water)
                {
                    dynamicMaxAge = maxAge - 1;
                }
            }
            if (!IsMature)
            {

                age += 1;
                if (animator != null)
                {
                    switch (age)
                    {
                        case 1:
                            animator.SetTrigger("smalltreeIdle");
                            break;
                        case 2:
                            animator.SetTrigger("middletreeIdle");
                            if (dynamicMaxAge == 2)
                            {
                                levelManager.AddAnimation(new AnimatorTimedAnim(animator, "grow"));
                            }
                            break;
                            break;
                        case 3:
                            {
                                animator.SetTrigger("bigtreeIdle");
                                if(dynamicMaxAge == 3)
                                {
                                    levelManager.AddAnimation(new AnimatorTimedAnim(animator, "grow"));
                                }
                                break;
                            }
                        case 4:
                            levelManager.AddAnimation(new AnimatorTimedAnim(animator, "grow"));
                            break;
                        default:
                            break;
                    }
                }

            }
            if (levelManager.Neighbour4TilesHasType(CellPos, TileObjectsReferences.fire))
            {
                //animator.SetTrigger("blockfire");
                animator.SetBool("blockfire", true);
            }
            else
            {
                //animator.SetTrigger("Idle");
                animator.SetBool("blockfire", false);
            }
        }

        public override void OnRoundEnd(LevelManager levelManager)
        {
            if (IsMature)
            {
                ExecuteTreeEffect(levelManager);
            }
        }

        /// <summary>
        /// 在 OnRoundEnd 触发
        /// </summary>
        /// <param name="levelManager"></param>
        public virtual void ExecuteTreeEffect(LevelManager levelManager)
        {

        }

        public override void Die(LevelManager levelManager, float delay = 0)
        {
            levelManager.RemoveObjActions(this);
            //播放死亡动画
            if (animator != null && IsMature)
            {
                levelManager.AddAnimation(new AnimatorTimedAnim(animator, "die", delay));
            }
            else
            {
                Destroy(gameObject, delay);
            }
        }

        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            //碰到火和挖掘机死亡
            foreach (TileObject tileObject in others)
            {
                if (tileObject.Is(TileObjectsReferences.fire) ||
                    tileObject.Is(TileObjectsReferences.excavatorForward))
                {
                    if(tileObject.Is(TileObjectsReferences.fire))
                    {
                        Die(levelManager);
                    }
                    else
                    {
                        Die(levelManager, 1.3f);
                    }
                }
            }
        }

        public override bool CanBeAddedAt(LevelManager levelManager, Vector3Int pos)
        {
            bool flag = true;
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.extinguishingTree);
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.fireBlockerTree);
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.madWillow);
            return flag && base.CanBeAddedAt(levelManager, pos);
        }
    }
}