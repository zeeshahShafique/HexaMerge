using _HexaMerge.Scripts.Shop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _HexaMerge.Scripts.UI.Cards
{
    public class RVAdCardView : CardView
    {
        [SerializeField] private AdSystem AdSystem;
        [SerializeField] private RewardHandler RewardHandler;
        [SerializeField] private Image RewardIcon;
        [SerializeField] private TextMeshProUGUI RewardAmount;

        private void Start()
        {
            SetRewardText();
            SetRewardSprite();
            SetRewardAmount();
        }

        protected override bool PurchaseItem()
        {
            DynamicOverlay.DisableShopOverlayCanvas();
            Button.interactable = true;
            return AdSystem.ShowRewardedAd(GiveRewards);
        }

        private void GiveRewards()
        {
            RewardHandler.ProcessRewards(Reward);
        }
        
        private void SetRewardSprite()
        {
            RewardIcon.sprite = Reward.Rewards[0].GetIcon();
        }
        
        private void SetRewardAmount()
        {
            RewardAmount.text = Reward.Rewards[0].GetAmount().ToString();
        }
    }
}