using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _HexaMerge.Scripts.UI
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LivesText;
        // [SerializeField] private TextMeshProUGUI TimerText;

        [SerializeField] private LivesSystem LivesSystem;

        [SerializeField] private Button AddLifeButton;
        [SerializeField] private DynamicOverlaySO DynamicOverlay;
        [SerializeField] private DynamicFeedbackSO DynamicFeedback;
        
        [SerializeField] private RectTransform SnapRect;

        private void OnEnable()
        {
            LivesSystem.ChangeLivesText += SetLivesView;
            
            AddLifeButton.onClick.AddListener(OnButtonPressed);
        }

        private void OnDisable()
        {
            LivesSystem.ChangeLivesText -= SetLivesView;
            
            AddLifeButton.onClick.RemoveListener(OnButtonPressed);
        }

        private void Start()
        {
            SetLivesView(LivesSystem.GetLives());
            DontDestroyOnLoad(this);
        }

        private void OnButtonPressed()
        {
            DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
            if (LivesSystem.IsFull())
            {
                DynamicOverlay.EnableClickableOverlay("Lives Already Full");
            }
            else
            {
                SnapRect.DOLocalMove(Vector3.left * 1125, 0.2f).SetEase(Ease.Flash).OnComplete(() =>
                {
                    DynamicOverlay.AddEnergyOverlay(SnapRect);
                });
            }
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                LivesSystem.SaveLivesPref();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                LivesSystem.InitLivesSystem();
        }
        
        private void SetLivesView(int amount)
        {
            LivesText.text = amount.ToString();
        }

        
    }
}