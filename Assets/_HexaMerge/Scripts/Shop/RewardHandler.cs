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

        
        public void PurchaseSuccess(IAPTypeSO iAPItem)
        {
            foreach (var reward in iAPItem.Rewards)
            {
                GiveReward(reward);
            }

            IAPCompleted?.Invoke(true);
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

        public void AddCoins(int amount)
        {
            CoinSystem.AddCoins(amount);
        }

        public void AddSkips(int amount)
        {
            SkipSystem.AddSkips(amount);
        }

        public void RemoveAds()
        {
            AdSystem.RemoveInter();
        }

        public void PurchaseFail(IAPTypeSO iAPItem)
        {
            IAPCompleted?.Invoke(false);
        }
    }
}