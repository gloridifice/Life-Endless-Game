using DG.Tweening;
using GGJ2023.Level;
using UnityEngine;
using UnityEngine.VFX;

namespace GGJ2023.TileObject
{
    public class FlowerTileObject : TileObject
    {
        public VisualEffect smokeVFX;
        public override void Init()
        {
            base.Init();
            smokeVFX.gameObject.SetActive(false);
            foreach (MeshRenderer meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.material.SetVector("_CenterPosition", transform.position);
            }
            transform.localScale = Vector3.zero;
            Tweener tweener = transform.DOScale(Vector3.one, 0.5f);
            tweener.SetEase(Ease.OutBounce);
            LevelManager.AddAnimation(new TweenTimedAnim(tweener, 0.2f));
        }

        public override bool CanBeAddedAt(LevelManager levelManager, Vector3Int pos)
        {
            bool flag1 = true;
            if (levelManager.HasAndGetTileObject(pos, out TileObject[] tiles))
            {
                foreach (TileObject tile in tiles)
                {
                    flag1 = flag1 && !(tile.Is(TileObjectsReferences.animalBack) || tile.Is(TileObjectsReferences.flower));
                }
            }

            return flag1 && base.CanBeAddedAt(levelManager, pos);
        }

        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            foreach (TileObject collideTile in others)
            {
                if (collideTile.Is(TileObjectsReferences.fire) || collideTile.Is(TileObjectsReferences.excavatorForward))
                {
                    Die(levelManager, 0.5f);
                    break;
                }
            }
        }

        public override void Die(LevelManager levelManager, float delay = 0)
        {
            levelManager.flowerCount--;
            smokeVFX.gameObject.SetActive(true);
            base.Die(levelManager, delay);
        }

        public override bool OnTileAdded(LevelManager levelManager, Vector3Int pos, out GameObject instance)
        {
            if (base.OnTileAdded(levelManager, pos, out instance))
            {
                levelManager.flowerCount++;
                return true;
            }

            return false;
        }
    }
}