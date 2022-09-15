using System;
using _HexaMerge.Scripts.DynamicFeedback;
using _HexaMerge.Scripts.Shop;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RestorePurchase : MonoBehaviour
{
    [Header("Button Configs")]
    [SerializeField] private Button Button;
    
    [Header("Purchase Module")]
    [SerializeField] private PurchaseModule PurchaseModule;

    [Header("Dynamic Overlay")]
    [SerializeField] private DynamicOverlaySO DynamicOverlay;

    [Header("Dynamic Feedback")] [SerializeField]
    private DynamicFeedbackSO DynamicFeedback;

    private void OnEnable()
    {
        Button.onClick.AddListener(OnRestorePurchaseButton);
        Button.onClick.AddListener(PlayFeedback);
    }

    private void OnDisable()
    {
        Button.onClick.RemoveListener(OnRestorePurchaseButton);
        Button.onClick.RemoveListener(PlayFeedback);
    }

    private void OnRestorePurchaseButton()
    {
        transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.75f);
        DynamicOverlay.EnableClickableOverlay(PurchaseModule.RestorePurchases()
            ? "Purchase Restored"
            : "No Internet Connection");
    }

    private void PlayFeedback()
    {
        DynamicFeedback.PlayAudioSource(DynamicAudio.CardClick);
        DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
    }
}
