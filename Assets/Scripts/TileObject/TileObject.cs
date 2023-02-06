using System;
using DG.Tweening;
using GGJ2023.Level;
using GGJ2023.MapTile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace GGJ2023.TileObject
{
    public class TileObject : MonoBehaviour
    {
        private Tilemap tilemap;
        public Tilemap Tilemap
        {
            get
            {
                if (tilemap == null)
                {
                    tilemap = transform.GetComponentInParent<Tilemap>();
                }

                return tilemap;
            }
        }


        public Animator animator;    //动画器


        protected LevelManager levelManager;

        public LevelManager LevelManager
        {
            get
            {
                if (levelManager == null)
                {
                    levelManager = transform.GetComponentInParent<LevelManager>();
                }

                return levelManager;
            }
        }

        [HideInInspector] public bool isDead;

        public static Vector3Int Null_Vector3_Int = new Vector3Int(Int32.MaxValue, Int32.MaxValue, Int32.MaxValue);
        private Vector3Int cellPos = Null_Vector3_Int;
        public Vector3Int CellPos
        {
            get
            {
                if (cellPos == Null_Vector3_Int)
                {
                    cellPos = Tilemap.layoutGrid.WorldToCell(transform.position);
                }

                return cellPos;
            }
            set { cellPos = value; }
        }

        /// <summary>
        /// 每一种瓦片物体都有一个唯一的 registryName, 作为区别不同种类瓦片物体的唯一标识符.
        /// 在新建预制体时必填!
        /// </summary>
        public string registryName;


        private void Awake()
        {
            if (string.IsNullOrEmpty(registryName))
            {
                Debug.LogError("Tile Object registryName is NULL!");
            }
        }

        public virtual void Start()
        {
            Init();
        }

        public virtual void Init()
        {
        }

        public virtual void OnRoundExecute(LevelManager levelManager)
        {
        }

        /// <summary>
        /// 返回是否能在对应位置放置方块，并输出实例。
        /// 只允许预制体执行该方法。
        /// </summary>
        public virtual bool OnTileAdded(LevelManager levelManager, Vector3Int pos, out GameObject instance)
        {
            instance = null;
            if (CanBeAddedAt(levelManager, pos))
            {
                instance = Instantiate(gameObject);
                return true;
            }

            return false;
        }

        public virtual bool CanBeAddedAt(LevelManager levelManager, Vector3Int pos)
        {
            return CanMoveTo(levelManager, pos);
        }

        public virtual void Move(Vector3Int displacement)
        {
            CellPos += displacement;
            Tweener tweener = transform.DOMove(tilemap.layoutGrid.CellToWorld(CellPos), 0.5f);
            tweener.SetEase(Ease.InOutQuart);
            tweener.SetEase(Ease.InOutQuart);
            LevelManager.AddAnimation(new TweenTimedAnim(tweener));
        }

        /// <summary>
        /// 判断物体是否超过边界，默认不能移动到墙体、空气上
        /// </summary>
        public virtual bool CanMoveTo(LevelManager levelManager, Vector3Int position)
        {
            bool flag = !levelManager.HasTileType(position, TileObjectsReferences.wall);
            flag = flag && levelManager.HasAndGetMapTile(position, out MapTile.MapTile mapTile);
            return flag;
        }

        public virtual void OnRoundEnd(LevelManager levelManager)
        {
        }

        /// <summary>
        /// 当同一个网格内有两个及以上物体时触发，在 OnRoundEnd 之后触发
        /// </summary>
        /// <param name="others"></param>
        public virtual void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
        }

        public virtual void Die(LevelManager levelManager, float delay = 0)
        {
            isDead = true;
            levelManager.RemoveObjActions(this);
            //播放死亡动画
            if (animator != null)
            {
                levelManager.AddAnimation(new AnimatorTimedAnim(animator, "die", delay));
            }
            else
            {
                Tweener tweener = transform.DOScale(Vector3.zero, 0.4f);
                tweener.SetEase(Ease.InBack);
                LevelManager.AddAnimation(new TweenTimedAnim(tweener, delay));
                tweener.onKill += () => Destroy(gameObject);
            }
            //DestroyImmediate(gameObject);
        }

        public virtual bool Is(RegistryTileObject reference)
        {
            return reference.registryName == registryName;
        }

    }
}