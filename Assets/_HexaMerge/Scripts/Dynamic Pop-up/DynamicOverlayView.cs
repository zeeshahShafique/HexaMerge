using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicOverlayView : MonoBehaviour
{
    [Header("Overlay Configs")]
    [SerializeField] private Button OverlayButton;
    [SerializeField] private RectTransform OverlayTransform;
    [SerializeField] private TextMeshProUGUI OverlayText;

    [Header("Overlay Scriptable Object")] [SerializeField]
    private DynamicOverlaySO DynamicOverlay;
    
    [Header("Tween Settings")]
    [SerializeField] private float OverlayTweenDuration;
    [SerializeField] private Ease OverlayTweenEase;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        OverlayButton.onClick.AddListener(OverlayButtonClicked);
        DynamicOverlay.EnableOverlayButton += EnableOverlayButton;
    }

    private void OnDisable()
    {
        OverlayButton.onClick.RemoveListener(OverlayButtonClicked);
        DynamicOverlay.EnableOverlayButton -= EnableOverlayButton;
    }

    private void OverlayButtonClicked()
    {
        ScaleDownOverlay();
    }

    private void ScaleDownOverlay()
    {
        OverlayTransform.DOScale(Vector3.zero, OverlayTweenDuration).SetEase(OverlayTweenEase).OnComplete(() =>
        {
            OverlayTransform.gameObject.SetActive(false);
        });
    }

    #if UNITY_EDITOR
    [ContextMenu("Invoke")]
    void EnableOverlay()
    {
        OverlayButton.gameObject.SetActive(true);
        OverlayTransform.DOScale(Vector3.one, OverlayTweenDuration).SetEase(OverlayTweenEase).OnComplete(() =>
        {
            OverlayText.text = "TestFunction";
            OverlayButton.interactable = true;
        });
    }
    #endif
    void EnableOverlayButton(string text)
    {
        OverlayText.text = text;
        OverlayButton.gameObject.SetActive(true);
        OverlayTransform.DOScale(Vector3.one, OverlayTweenDuration).SetEase(OverlayTweenEase).OnComplete(() =>
        {
            OverlayButton.interactable = true;
        });
    }
}
