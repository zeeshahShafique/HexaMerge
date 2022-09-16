using _HexaMerge.Scripts.DynamicFeedback;
using _HexaMerge.Scripts.Shop;
using UnityEngine;

namespace _HexaMerge.Scripts.UI.Cards
{
    public abstract class StoreItemCardView : CardView
    {
        [SerializeField] private PurchaseModule PurchaseModule;

        protected override void OnIAPButtonPressed()
        {
            Debug.LogError($"Purchase Module Called");
            base.OnIAPButtonPressed();
        }

        protected override bool PurchaseItem()
        {
            if (!PurchaseModule.MakePurchase(Reward)) return false;
            PurchaseModule.AddPurchaseCompleteAction(IAPCompleted);
            return true;
        }
        
        protected virtual void IAPCompleted(bool flag)
        {
            var text = flag ? $"Transaction Successful\n{Reward.Title}" : $"Transaction Cancelled\n{Reward.Title}";
            
            DynamicOverlay.EnableClickableOverlay(text);
            
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.Success);
            
            // ShopOverlayCanvas.gameObject.SetActive(false);
            PurchaseModule.RemovePurchaseCompleteAction(IAPCompleted);
            Button.interactable = true;
        }
    }
}