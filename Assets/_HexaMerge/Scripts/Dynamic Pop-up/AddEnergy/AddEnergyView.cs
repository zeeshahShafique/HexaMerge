using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AddEnergyView : MonoBehaviour
{
    [SerializeField] private Button RefillButton;

    [SerializeField] private Button FreeRefillButton;

    [SerializeField] private Button CloseButton;

    [SerializeField] private RectTransform AddEnergyPanel;

    [SerializeField] private EnergySystem EnergySystem;

    [SerializeField] private DynamicOverlaySO DynamicOverlay;

    [SerializeField] private RectTransform SnapRect;

    private void EnableAddEnergyPanel(RectTransform snapRect)
    {
        SnapRect = snapRect;
        AddEnergyPanel.gameObject.SetActive(true);
        AddEnergyPanel.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.Flash);
    }

    private void OnEnable()
    {
        RefillButton.onClick.AddListener(OnRefillButton);
        FreeRefillButton.onClick.AddListener(OnFreeRefillButton);
        CloseButton.onClick.AddListener(OnCloseButton);
        DynamicOverlay.EnableAddEnergyOverlay += EnableAddEnergyPanel;
    }

    private void OnDisable()
    {
        RefillButton.onClick.RemoveListener(OnRefillButton);
        FreeRefillButton.onClick.RemoveListener(OnFreeRefillButton);
        CloseButton.onClick.RemoveListener(OnCloseButton);
        DynamicOverlay.EnableAddEnergyOverlay -= EnableAddEnergyPanel;
    }

    private void OnRefillButton()
    {
        EnergySystem.BuyEnergy();
        Invoke(nameof(OnCloseButton), 0.2f);
    }

    private void OnFreeRefillButton()
    {
        EnergySystem.AddRewardAdLives();
        Invoke(nameof(OnCloseButton), 0.2f);
    }

    private void OnCloseButton()
    {
        AddEnergyPanel.DOLocalMove(Vector3.right * 1125, 0.2f).SetEase(Ease.Flash).OnComplete(() =>
        {
            AddEnergyPanel.gameObject.SetActive(false);
            SnapRect.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.Flash);
        });
    }
    
}
