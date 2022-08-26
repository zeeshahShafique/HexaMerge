using UnityEngine;
using UnityEngine.Events;

namespace _Hexa_Merge.Scripts.RandomGenerator
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
