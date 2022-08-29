using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator
{
    public class SkipSpawnedTile : MonoBehaviour
    {
        public delegate void SkipGeneratedTile();

        public static event SkipGeneratedTile OnSkip;
        public void SkipTile()
        {
            OnSkip?.Invoke();
        }
    }
}
