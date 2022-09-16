using TMPro;
using UnityEngine;

namespace _HexaMerge.Scripts.UI
{
    public class LivesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI LivesText;
        [SerializeField] private TextMeshProUGUI TimerText;

        [SerializeField] private LivesSystem LivesSystem;
        
        private void OnEnable()
        {
            LivesSystem.ChangeLivesText += SetLivesView;
            LivesSystem.ChangeTimerText += UpdateTimerText;
            LivesSystem.SetFullText += UpdateText;
        }

        private void OnDisable()
        {
            LivesSystem.ChangeLivesText -= SetLivesView;
            LivesSystem.ChangeTimerText -= UpdateTimerText;
            LivesSystem.SetFullText -= UpdateText;
        }

        private void Start()
        {
            SetLivesView(LivesSystem.GetLives());
            DontDestroyOnLoad(this);
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

        private void UpdateTimerText(int timer)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            
            TimerText.text = $"{minutes:00}:{seconds:00}";
        }

        private void UpdateText()
        {
            TimerText.text = "FULL";
        }
    }
}