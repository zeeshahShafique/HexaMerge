using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _HexaMerge.Scripts.UI.Cards
{
    public abstract class CardView : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI RewardText;

        [SerializeField] protected TextMeshProUGUI RewardPrice;
        
        [SerializeField] protected IAPTypeSO Reward;
        
        [SerializeField] protected DynamicOverlaySO DynamicOverlay;

        [SerializeField] protected DynamicFeedbackSO DynamicFeedback;

        [Header("IAP Button")]
        [SerializeField] protected Button Button;


        public void SetReward(IAPTypeSO reward)
        {
            Reward = reward;
        }
        
        protected void SetRewardText()
        {
            RewardText.text = Reward.Title;
        }
        
        protected void SetRewardPrice()
        {
            RewardPrice.text = Reward.Price.ToString();
        }
        
        private void OnEnable()
        {
            // ShopOverlayCanvas = FindObjectOfType<Canvas>();
            Button.onClick.AddListener(OnIAPButtonPressed);
            Button.onClick.AddListener(PlayFeedback);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnIAPButtonPressed);
            Button.onClick.RemoveListener(PlayFeedback);
        }
        
        private void PlayFeedback()
        {
            DynamicFeedback.PlayAudioSource(DynamicAudio.CardClick);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
        }
        
        protected virtual void OnIAPButtonPressed()
        {
            Button.interactable = false;
            this.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 1f).OnComplete(() =>
            {
                DynamicOverlay.EnableShopOverlayCanvas();
                if (PurchaseItem())
                {
                    return;
                }
                DynamicOverlay.DisableShopOverlayCanvas();
                Button.interactable = true;
            });
        }

        protected virtual bool PurchaseItem()
        {
            return false;
        }
    }
}