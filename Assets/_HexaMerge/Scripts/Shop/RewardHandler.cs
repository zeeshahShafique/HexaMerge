using System;
using _HexaMerge.Scripts.RandomGenerator;
using _HexaMerge.Scripts.Shop.Enum;
using UnityEngine;

namespace _HexaMerge.Scripts.Shop
{
    [CreateAssetMenu(menuName = "ScriptableObject/IAPs/RewardHandler", order = 1)]
    public class RewardHandler : ScriptableObject, IItemPurchase
    {
        [SerializeField] private AdSystem AdSystem;
        [SerializeField] private Coins CoinSystem;
        [SerializeField] private SkipSpawnedTile SkipSystem;
        
        public Action<bool> IAPCompleted;

        public void ProcessRewards(IAPTypeSO iAPItem)
        {
            foreach (var reward in iAPItem.Rewards)
            {
                GiveReward(reward);
            }
        }

        
        public void PurchaseSuccess(IAPTypeSO iAPItem)
        {
            IAPCompleted?.Invoke(true);
            AdSystem.ResetInterTimer();
        }

        private void GiveReward(Reward reward)
        {
            switch (reward.GetRewardType())
            {
                case RewardType.Coins:
                    AddCoins(reward.GetAmount());
                    break;
                case RewardType.Skips:
                    AddSkips(reward.GetAmount());
                    break;
                case RewardType.NoAds:
                    RemoveAds();
                    break;
            }
        }

        private void AddCoins(int amount)
        {
            CoinSystem.AddCoins(amount);
        }

        private void AddSkips(int amount)
        {
            SkipSystem.AddSkips(amount);
        }

        private void RemoveAds()
        {
            AdSystem.RemoveInter();
            PlayerPrefs.SetInt("RemoveAds", 1);
            PlayerPrefs.Save();
        }

        public void PurchaseFail(IAPTypeSO iAPItem)
        {
            IAPCompleted?.Invoke(false);
        }
    }
}