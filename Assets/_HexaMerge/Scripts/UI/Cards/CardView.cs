using _HexaMerge.Scripts.Shop;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
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
            PurchaseModule.AddPurchaseCompleteAction(IAPCompleted);
        }

        private void IAPCompleted(bool flag)
        {
            PurchaseModule.RemovePurchaseCompleteAction(IAPCompleted);
            Button.interactable = true;

        }

    }
}