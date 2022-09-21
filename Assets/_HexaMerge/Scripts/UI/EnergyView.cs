using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _HexaMerge.Scripts.UI
{
    public class EnergyView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LivesText;

        [SerializeField] private EnergySystem EnergySystem;

        [SerializeField] private Button AddLifeButton;
        [SerializeField] private DynamicOverlaySO DynamicOverlay;
        [SerializeField] private DynamicFeedbackSO DynamicFeedback;
        
        [SerializeField] private RectTransform SnapRect;

        private void OnEnable()
        {
            EnergySystem.ChangeLivesText += SetLivesView;
            
            AddLifeButton.onClick.AddListener(OnButtonPressed);
        }

        private void OnDisable()
        {
            EnergySystem.ChangeLivesText -= SetLivesView;
            
            AddLifeButton.onClick.RemoveListener(OnButtonPressed);
        }

        private void Start()
        {
            SetLivesView(EnergySystem.GetLives());
            DontDestroyOnLoad(this);
        }

        private void OnButtonPressed()
        {
            DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
            if (EnergySystem.IsFull())
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
                EnergySystem.SaveLivesPref();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
                EnergySystem.InitLivesSystem();
        }
        
        private void SetLivesView(int amount)
        {
            LivesText.text = amount.ToString();
        }

        
    }
}