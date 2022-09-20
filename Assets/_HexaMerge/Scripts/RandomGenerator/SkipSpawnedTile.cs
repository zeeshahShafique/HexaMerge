using System;
using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator
{
    [CreateAssetMenu(menuName = "ScriptableObject/SkipTileSystem/SkipTile", order = 1)]
    public class SkipSpawnedTile : ScriptableObject
    {
        [SerializeField] private int SkipAmount = 5;
        [SerializeField] private String SkipPrefKey;
        [SerializeField] private int AdSkipsAmount;
        public delegate void SkipGeneratedTile();
        public static event SkipGeneratedTile OnSkip;

        [SerializeField] private AdSystem AdSystem;
        
        public Action<int> ChangeSkipText;


        private void OnEnable()
        {
            if (PlayerPrefs.HasKey(SkipPrefKey))
                SkipAmount = PlayerPrefs.GetInt(SkipPrefKey);
        }
        

        private void SaveSkipPrefs()
        {
            PlayerPrefs.SetInt(SkipPrefKey, SkipAmount);
            PlayerPrefs.Save();
        }

        public void SkipTile()
        {
            Skip();
            SkipAmount--;
            SaveSkipPrefs();
            ChangeSkipText?.Invoke(SkipAmount);
        }

        public void AddSkipsThroughAd()
        {
            AdSystem.ShowRewardedAd(AddSkipsAd);
        }

        private void AddSkipsAd()
        {
            AddSkips(AdSkipsAmount);
        }

        void Skip()
        {
            OnSkip?.Invoke();
        }

        public void AddSkips(int amount)
        {
            SkipAmount += amount;
            SaveSkipPrefs();
            ChangeSkipText?.Invoke(SkipAmount);
        }

        public int GetSkips()
        {
            return SkipAmount;
        }
    }
}
