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
        
        [SerializeField] private RectTransform TransactionOverlay;

        [SerializeField] private TextMeshProUGUI TransactionText;

        [Header("Shop Overlay Canvas")] [SerializeField]
        private Canvas ShopOverlay;
        
        [Header("IAP Button")]
        [SerializeField] private Button Button;
        
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
            Button.onClick.AddListener(OnIAPButtonPressed);
            
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(OnIAPButtonPressed);
        }


        private void OnIAPButtonPressed()
        {
            Debug.LogError($"Purchase Module Called");
            Button.interactable = false;
            PurchaseModule.MakePurchase(Reward);
            ShopOverlay.gameObject.SetActive(true);
            PurchaseModule.AddPurchaseCompleteAction(IAPCompleted);
            this.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 1f);
        }

        private void IAPCompleted(bool flag)
        {
            if (flag)
                TransactionText.text = $"Transaction Successful\n{Reward.Title}";
            else
                TransactionText.text = $"Transaction Cancelled\n{Reward.Title}";
            TransactionOverlay.DOScale(Vector3.one, 0.2f).OnComplete(() =>
            {
                TransactionOverlay.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad).SetDelay(1.5f);
            }).SetEase(Ease.InQuad);
            PurchaseModule.RemovePurchaseCompleteAction(IAPCompleted);
            Button.interactable = true;
            ShopOverlay.gameObject.SetActive(false);

        }

    }
}