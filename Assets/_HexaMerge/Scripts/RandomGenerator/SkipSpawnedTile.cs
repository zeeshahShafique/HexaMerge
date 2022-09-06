using System;
using System.Collections;
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
            if (AdSystem.ShowRewardedAd())
            {
                StartCoroutine(Skip());
            }
        }

        IEnumerator Skip()
        {
            while (!AdSystem.RewardReceived)
            {
                yield return null;
            }
            OnSkip?.Invoke();
        }
    }
}
