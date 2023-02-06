using UnityEngine;

namespace GGJ2023.Level
{
    public enum DirectionType
    {
        Forward,
        Right,
        Back,
        Left,
        Up,
        Down
    }

    public class DirectionTypeUtils
    {
        public static Vector3Int
            forward = new(0, 1, 0),
            right = new(1, 0, 0),
            back = new(0, -1, 0),
            left = new(-1, 0, 0);

        public static Vector3Int GetVectorFromDirection(DirectionType type)
        {
            switch (type)
            {
                case DirectionType.Forward: return forward;
                case DirectionType.Right: return right;
                case DirectionType.Back: return back;
                case DirectionType.Left: return left;
                case DirectionType.Up: return Vector3Int.up;
                case DirectionType.Down: return Vector3Int.down;
                default: return Vector3Int.forward;
            }
        }

        public static DirectionType GetOpposite(DirectionType direction)
        {
            DirectionType opposite = direction;
            if (opposite == DirectionType.Forward)
                opposite = DirectionType.Back;
            else if(opposite == DirectionType.Back)
                opposite = DirectionType.Forward;
            else if (opposite == DirectionType.Left)
                opposite = DirectionType.Right;
            else if (opposite == DirectionType.Right)
                opposite = DirectionType.Left;
            return opposite;
        }
        /// <summary>
        /// 返回从前方一格开始的八个邻居
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static Vector3Int[] Get8Neighbour(Vector3Int pos)
        {
            Vector3Int v0 = pos + forward;
            Vector3Int v1 = pos + forward + right;
            Vector3Int v2 = pos + right;
            Vector3Int v3 = pos + back + right;
            Vector3Int v4 = pos + back;
            Vector3Int v5 = pos + back + left;
            Vector3Int v6 = pos + left;
            Vector3Int v7 = pos + left + forward;
            return new[] { v0, v1, v2, v3, v4, v5, v6, v7 };
        }

        public static Vector3Int[] Get4Neighbour(Vector3Int pos)
        {
            Vector3Int v0 = pos + forward;
            Vector3Int v2 = pos + right;
            Vector3Int v4 = pos + back;
            Vector3Int v6 = pos + left;
            return new[] { v0, v2, v4, v6 };
        }
    }
}