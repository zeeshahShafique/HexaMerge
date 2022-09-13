using System;
using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator
{
    [CreateAssetMenu(menuName = "ScriptableObject/SkipTileSystem", order = 1)]
    public class SkipSpawnedTile : ScriptableObject
    {
        [SerializeField] private int SkipAmount;
        [SerializeField] private String SkipPrefKey;
        public delegate void SkipGeneratedTile();
        public static event SkipGeneratedTile OnSkip;

        [SerializeField] private AdSystem AdSystem;

        private void OnEnable()
        {
            SkipAmount = PlayerPrefs.GetInt(SkipPrefKey);
        }

        private void OnDisable()
        {
            PlayerPrefs.SetInt(SkipPrefKey, SkipAmount);
            PlayerPrefs.Save();
        }

        public void SkipTile()
        {
            if (SkipAmount > 0)
                AdSystem.ShowRewardedAd(Skip);
        }

        void Skip()
        {
            OnSkip?.Invoke();
            SkipAmount--;
        }

        public void AddSkips(int amount)
        {
            SkipAmount += amount;
        }

        public int GetSkips()
        {
            return SkipAmount;
        }
    }
}
