using TMPro;
using UnityEngine;

namespace _HexaMerge.Scripts.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimerText;

        [SerializeField] private LivesSystem LivesSystem;

        private void OnEnable()
        {
            LivesSystem.ChangeTimerText += UpdateTimerText;
            LivesSystem.SetFullText += UpdateText;
        }

        private void OnDisable()
        {
            LivesSystem.ChangeTimerText -= UpdateTimerText;
            LivesSystem.SetFullText -= UpdateText;
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