using GGJ2023.Level;
using UnityEngine;

namespace GGJ2023.TileObject
{
    public class AnimalTileObject : DirectionalTileObject
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
                else if(collideTile.Is(TileObjectsReferences.tumbleweed))   //碰到风滚草播放攻击动画
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
                //更改朝向
                /*
                Vector3 scale = transform.localScale;
                scale.x = -scale.x;
                transform.localScale = scale;
                */
            }
        }
    }
}