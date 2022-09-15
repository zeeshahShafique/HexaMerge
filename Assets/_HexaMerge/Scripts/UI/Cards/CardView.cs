using _HexaMerge.Scripts.DynamicFeedback;
using _HexaMerge.Scripts.Shop;
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

        [SerializeField] private PurchaseModule PurchaseModule;

        [SerializeField] private DynamicOverlaySO DynamicOverlay;

        [SerializeField] private DynamicFeedbackSO DynamicFeedback;

        // [SerializeField] private Canvas ShopOverlayCanvas;
        
        [Header("IAP Button")]
        [SerializeField] private Button Button;


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


        private void OnIAPButtonPressed()
        {
            Debug.LogError($"Purchase Module Called");
            Button.interactable = false;
            if (!PurchaseModule.MakePurchase(Reward))
            {
                Button.interactable = true;
                return;
            }
            // StartCoroutine(nameof(Purchase));
            // ShopOverlayCanvas.gameObject.SetActive(true);
            PurchaseModule.AddPurchaseCompleteAction(IAPCompleted);
            this.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 1f);
        }

        // private IEnumerator Purchase()
        // {
        //     yield return new WaitForSeconds(0.2f);
        //     PurchaseModule.MakePurchase(Reward);
        // }

        protected virtual void IAPCompleted(bool flag)
        {
            var text = flag ? $"Transaction Successful\n{Reward.Title}" : $"Transaction Cancelled\n{Reward.Title}";
            
            DynamicOverlay.EnableClickableOverlay(text);
            
            // ShopOverlayCanvas.gameObject.SetActive(false);
            PurchaseModule.RemovePurchaseCompleteAction(IAPCompleted);
            Button.interactable = true;
        }

    }
}