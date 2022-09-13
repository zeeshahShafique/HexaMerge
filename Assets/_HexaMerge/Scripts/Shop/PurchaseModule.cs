using System;
using UnityEngine;

namespace _HexaMerge.Scripts.Shop
{
    [CreateAssetMenu(menuName = "ScriptableObject/IAPs/PurchaseModule", order = 1)]
    public class PurchaseModule : ScriptableObject
    {
        [SerializeField] private StoreSO Store;

        [SerializeField]
        private RewardHandler Rewards;
        // [SerializeField] private Rect 

        public void MakePurchase(IAPTypeSO iap)
        {
            Store.PurchaseItemWithId(iap.SKU, Rewards);
        }

        public void AddPurchaseCompleteAction(Action<bool> purchaseComplete)
        {
            Rewards.IAPCompleted += purchaseComplete;
        }
        
        public void RemovePurchaseCompleteAction(Action<bool> purchaseComplete)
        {
            Rewards.IAPCompleted -= purchaseComplete;
        }
    }
}