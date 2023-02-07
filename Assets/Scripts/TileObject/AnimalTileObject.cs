using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class AnimalTileObject : DirectionalTileObject
    {
        public override void OnRoundExecute(LevelManager levelManager)
        {
            if (!CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                moveDirection = DirectionTypeUtils.GetOpposite(moveDirection);
            }

            if (CanMoveTo(levelManager, CellPos + DirectionTypeUtils.GetVectorFromDirection(moveDirection)))
            {
                levelManager.AddAnimation(new AnimatorTimedAnim(animator, "move"));
                Move(DirectionTypeUtils.GetVectorFromDirection(moveDirection));
            }
        }

        public override void OnTileCollide(LevelManager levelManager, TileObject[] others, Vector3Int pos)
        {
            foreach (TileObject collideTile in others)
            {
                if (collideTile.Is(TileObjectsReferences.fire) || collideTile.Is(TileObjectsReferences.excavatorForward))
                {
                    Die(levelManager);
                }
                else if(collideTile.Is(TileObjectsReferences.tumbleweed))   //Åöµ½·ç¹ö²Ý²¥·Å¹¥»÷¶¯»­
                {
                    levelManager.AddAnimation(new AnimatorTimedAnim(animator, "attack"));
                }
            }
        }
    }
}