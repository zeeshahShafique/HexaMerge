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
        
        [SerializeField] private DynamicOverlaySO DynamicOverlay;


        public bool MakePurchase(IAPTypeSO iap)
        {
            return Store.PurchaseItemWithId(iap.SKU, Rewards);
        }

        public void AddPurchaseCompleteAction(Action<bool> purchaseComplete)
        {
            Rewards.IAPCompleted += purchaseComplete;
        }
        
        public void RemovePurchaseCompleteAction(Action<bool> purchaseComplete)
        {
            DynamicOverlay.DisableShopOverlayCanvas();
            Rewards.IAPCompleted -= purchaseComplete;
        }

        public bool RestorePurchases()
        {
            return Store.RestorePurchases();
        }
    }
}