using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class CoinView : MonoBehaviour
{
    [SerializeField] private Coins Coins;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private HorizontalScrollSnap Snap;
    [SerializeField] private DynamicFeedbackSO DynamicFeedback;

    [SerializeField] private Button ShopButton;

    private void Awake()
    {
        ChangeCoinView(Coins.GetCoins());
    }

    private void OnEnable()
    {
        Coins.ChangeCoinText += ChangeCoinView;
        ShopButton.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        Coins.ChangeCoinText -= ChangeCoinView;
        ShopButton.onClick.RemoveListener(OnButtonClicked);
    }

    private void ChangeCoinView(int amount)
    {
        CoinText.text = amount.ToString();
    }

    private void OnButtonClicked()
    {
        ShopButton.interactable = false;
        DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
        DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);

        transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 2, 1f).OnComplete(() =>
        {
            ShopButton.interactable = true;
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Snap.ChangePage(0);
            }
        });
    }

}
