using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _HexaMerge.Scripts.RandomGenerator.Interface
{
    public class SkipTileView: MonoBehaviour
    {
        [SerializeField] private SkipSpawnedTile SkipSystem;
        [SerializeField] private TextMeshProUGUI SkipText;

        [SerializeField] private DynamicFeedbackSO DynamicFeedback;
        [SerializeField] private RectTransform SkipOverlay;
        
        [SerializeField] private Button SkipButton;

        private void OnEnable()
        {
            SkipSystem.ChangeSkipText += ChangeSkipView;
            SkipButton.onClick.AddListener(OnSkipButtonCalled);
        }

        private void OnDisable()
        {
            SkipSystem.ChangeSkipText -= ChangeSkipView;
            SkipButton.onClick.RemoveListener(OnSkipButtonCalled);
        }

        private void OnSkipButtonCalled()
        {
            DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
            if (SkipSystem.GetSkips() == 0)
            {
                SkipOverlay.gameObject.SetActive(true);
            }
            else
            {
                SkipSystem.SkipTile();
            }
        }

        private void ChangeSkipView(int amount)
        {
            if (amount > 0)
                SkipText.text = amount.ToString();
            else
                SkipText.text = "+";
        }

        private void Awake()
        {
            ChangeSkipView(SkipSystem.GetSkips());
        }
        
    }
}