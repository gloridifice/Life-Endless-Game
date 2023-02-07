using System;
using GGJ2023.Audio;
using GGJ2023.Level;
using GGJ2023.MapTile;
using UnityEngine;
using UnityEngine.VFX;

namespace GGJ2023.TileObject
{
    public class FireTileObject : TileObject
    {
        public AudioSource audioSource;

        private int strength = 0;
        
        /// <summary>
        /// 在 fireRound 的下一回合生成火苗
        /// </summary>
        private int fireRound = 3;

        public bool IsMature => strength == fireRound;

        public override void Init()
        {
            base.Init();
            UpdateExterior();
        }

        /// <summary>
        /// 更新火焰的外观
        /// </summary>
        private void UpdateExterior()
        {
            VisualEffect effect = GetComponentInChildren<VisualEffect>();
            if (effect != null)
            {
                effect.SetFloat("VolumeSize", ((float)strength + 1) / ((float)fireRound + 2));
                effect.SetInt("IsMature", IsMature ? 1 : 0);
            }
        }
        public override void OnRoundExecute(LevelManager levelManager)
        {
            base.OnRoundExecute(levelManager);
            if (IsMature)
            {
                audioSource.Play();
                Vector3Int newFirePos = DirectionTypeUtils.GetVectorFromDirection(levelManager.windDirection) + CellPos;
                levelManager.AddTileObject(newFirePos, TileObjectsReferences.fire);
            }
            else
            {
                strength++;
            }
            UpdateExterior();
        }

        public override bool CanBeAddedAt(LevelManager levelManager, Vector3Int pos)
        {
            bool flag = true;
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.fire);
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.fireBlockerTree);
            flag = flag && !levelManager.HasTileType(pos, TileObjectsReferences.wall);
            //不能蔓延到水源上
            if (levelManager.HasAndGetMapTile(pos, out MapTile.MapTile tile))
            {
                if (tile.type == MapTileType.Water)
                {
                    flag = false;
                }
            }
            return flag && base.CanBeAddedAt(levelManager, pos);
        }
    }
}