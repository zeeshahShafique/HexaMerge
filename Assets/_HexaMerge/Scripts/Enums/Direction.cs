using UnityEngine;

namespace _HexaMerge.Scripts.Enums
{
    public static class Direction
    {
        public static readonly Vector2 TopRight = new Vector2(1, -1);
        public static readonly Vector2 Right = new (1,0);
        public static readonly Vector2 BottomRight = new (0,1);
        
        public static readonly Vector2 TopLeft = new(0, -1);
        public static readonly Vector2 Left = new (-1,0);
        public static readonly Vector2 BottomLeft = new (-1,1);
    }
}