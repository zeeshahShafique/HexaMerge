using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator
{
    public class SkipSpawnedTile : MonoBehaviour
    {
        public delegate void SkipGeneratedTile();
        public static event SkipGeneratedTile OnSkip;

        [SerializeField] private AdSystem AdSystem;
        
        public void SkipTile()
        {
            AdSystem.ShowRewardedAd(Skip);
        }

        void Skip()
        {
            OnSkip?.Invoke();
        }
    }
}
