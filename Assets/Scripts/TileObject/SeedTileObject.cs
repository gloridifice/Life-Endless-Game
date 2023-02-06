using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class SeedTileObject : TileObject
    {
        [SerializeField] private string treeName;
        private RegistryTileObject treeType;

        public RegistryTileObject TreeType
        {
            get
            {
                if (treeType == null)
                {
                    treeType = TileObjectsReferences.GetTileObjectFromName(treeName);
                }

                return treeType;
            }
        }

        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            foreach (var tileObject in others)
            {
                if (tileObject.Is(TileObjectsReferences.tumbleweed))
                {
                    levelManager.AddSeed(TreeType);
                    Die(levelManager);
                }

                if (tileObject.Is(TileObjectsReferences.excavatorBack) || tileObject.Is(TileObjectsReferences.fire))
                {
                    Die(levelManager);
                }
            }
        }
    }
}