using _HexaMerge.Scripts.DynamicFeedback;
using _HexaMerge.Scripts.RandomGenerator;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AddSkipsView : MonoBehaviour
{
    [Header("Object Transforms")] [SerializeField]
    private Transform TouchSystemTransform;

    [SerializeField] private Transform GridSystemTransform;

    [SerializeField] private Transform ButtonsTransform;

    [SerializeField] private Transform TileControllerTransform;

    [Header("Buttons")]
    [SerializeField] private Button CloseButton;

    [SerializeField] private Button FreeSkipButton;

    [SerializeField] private Button BuySkipsButton;

    [Header("Config")]
    [SerializeField] private DynamicFeedbackSO DynamicFeedback;

    [SerializeField] private SkipSpawnedTile SkipSystem;

    private Vector3 _touchSystemCachePos;

    // Start is called before the first frame update

    private void OnEnable()
    {
        _touchSystemCachePos = TouchSystemTransform.localPosition;
        CloseButton.onClick.AddListener(OnCLoseButtonCalled);
        FreeSkipButton.onClick.AddListener(OnFreeSkipButton);
        BuySkipsButton.onClick.AddListener(OnBuySkipsButton);
        

        TouchSystemTransform.DOLocalMove(Vector3.left * 100, 0.5f).SetEase(Ease.OutCirc);
        GridSystemTransform.DOLocalMove(Vector3.left * 100, 0.5f).SetEase(Ease.OutCirc);
        ButtonsTransform.DOLocalMove(Vector3.left * 3000, 0.5f).SetEase(Ease.OutCirc);
        TileControllerTransform.DOLocalMove(Vector3.left * 3000, 0.5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InCirc);
        });
    }

    private void OnDisable()
    {
        CloseButton.onClick.RemoveListener(OnCLoseButtonCalled);
        FreeSkipButton.onClick.RemoveListener(OnFreeSkipButton);
        BuySkipsButton.onClick.RemoveListener(OnBuySkipsButton);
    }

    private void OnFreeSkipButton()
    {
        SkipSystem.AddSkipsThroughAd();
        OnCLoseButtonCalled();
    }

    private void OnBuySkipsButton()
    {
        SkipSystem.BuySkips();
        OnCLoseButtonCalled();
    }

    private void OnCLoseButtonCalled()
    {
        DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
        DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
        CloseButton.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 5, 0.75f);

        transform.DOLocalMove(Vector3.right * 1125, 0.5f).SetEase(Ease.OutCirc).OnComplete(() =>
        {
            GridSystemTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InCirc);
            ButtonsTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InCirc);
            TouchSystemTransform.DOLocalMove(_touchSystemCachePos, 0.5f).SetEase(Ease.InCirc);
            TileControllerTransform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InCirc);
            this.gameObject.SetActive(false);
        });
    }
}
