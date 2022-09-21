using TMPro;
using UnityEngine;

namespace _HexaMerge.Scripts.UI
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI TimerText;

        [SerializeField] private EnergySystem EnergySystem;

        private void OnEnable()
        {
            EnergySystem.ChangeTimerText += UpdateTimerText;
            EnergySystem.SetFullText += UpdateText;
        }

        private void OnDisable()
        {
            EnergySystem.ChangeTimerText -= UpdateTimerText;
            EnergySystem.SetFullText -= UpdateText;
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