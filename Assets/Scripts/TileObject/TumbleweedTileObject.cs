using System.Collections.Generic;
using GGJ2023.Level;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

namespace GGJ2023.TileObject
{
    public class TumbleweedTileObject : TileObject
    {
        public override void Init()
        {
            base.Init();

            Invoke("AddFlower", 0.1f);
        }
        void AddFlower()
        {
            if (TileObjectsReferences.flower.prefab != null)
            {
                LevelManager levelManager = transform.GetComponentInParent<LevelManager>();
                levelManager.AddTileObject(CellPos, TileObjectsReferences.flower);
            }
        }
        public override void OnRoundExecute(LevelManager levelManager)
        {
            Vector3 scale = transform.localScale;
            if (levelManager.windDirection == DirectionType.Right) scale.x = -Mathf.Abs(scale.x);
            if (levelManager.windDirection == DirectionType.Left) scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            
            if (WillBumpedIntoAnimal(levelManager))
            {
                Die(levelManager);
                return;
            }
            Vector3Int direction = DirectionTypeUtils.GetVectorFromDirection(levelManager.windDirection);

            levelManager.AddAnimation(new AnimatorTimedAnim(animator, "roll", 0.14f));     //播放翻滚动画

            if (CanMoveTo(levelManager, CellPos + direction))
            {
                Move(direction);
                levelManager.AddTileObject(CellPos, TileObjectsReferences.flower);
            }
        }

        bool WillBumpedIntoAnimal(LevelManager levelManager)
        {
            if (levelManager.HasAndGetNeighbour4Tiles(CellPos, out Dictionary<DirectionType, TileObject[]> list))
            {
                foreach (var objects in list)
                {
                    foreach (TileObject tileObject in objects.Value)
                    {
                        //是动物且风向和动物的方向一致
                        if (tileObject.Is(TileObjectsReferences.animalBack) &&
                            objects.Key == levelManager.windDirection)
                        {
                            AnimalTileObject o = (AnimalTileObject)tileObject;
                            //玩家移动方向和动物移动方向对立
                            if (o.moveDirection == DirectionTypeUtils.GetOpposite(levelManager.windDirection))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            foreach (TileObject collideTile in others)
            {
                if (collideTile.Is(TileObjectsReferences.fire))
                {
                    Die(levelManager);  //游戏结束并死亡
                }
                else if (collideTile.Is(TileObjectsReferences.excavatorForward) ||
                    collideTile.Is(TileObjectsReferences.animalBack))
                {
                    Die(levelManager, 2f);  //游戏结束并死亡
                }
            }
        }
        public override void Die(LevelManager levelManager ,float delay = 0)
        {
            if (gameObject != null)
            {
                //播放死亡动画
                if (animator != null)
                {
                    levelManager.AddAnimation(new AnimatorTimedAnim(animator, "die", delay));
                    StartCoroutine(WaitDieStop(levelManager));
                }
                else
                {
                    DestroyImmediate(gameObject);
                    levelManager.GameOver();
                }
                //levelManager.GameOver();
            }
        }

        IEnumerator<int> WaitDieStop(LevelManager levelManager)
        {
            yield return 0;
            if(animator!=null)
            { 
                AnimatorStateInfo stateinfo = animator.GetCurrentAnimatorStateInfo(0);

                //死亡动画结束后，游戏结束
                if (stateinfo.IsName("die") && (stateinfo.normalizedTime > 0.9f))
                {
                    levelManager.GameOver();
                }
                else
                {
                    StartCoroutine(WaitDieStop(levelManager));
                }
            }
        }
    }
}