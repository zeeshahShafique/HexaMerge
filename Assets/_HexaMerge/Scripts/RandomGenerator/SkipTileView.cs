using TMPro;
using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator.Interface
{
    public class SkipTileView: MonoBehaviour
    {
        [SerializeField] private SkipSpawnedTile SkipSystem;
        [SerializeField] private TextMeshProUGUI SkipText;

        private void OnEnable()
        {
            SkipSystem.ChangeSkipText += ChangeSkipView;
        }

        private void OnDisable()
        {
            SkipSystem.ChangeSkipText -= ChangeSkipView;
        }

        private void ChangeSkipView(int amount)
        {
            SkipText.text = amount.ToString();
        }

        private void Awake()
        {
            ChangeSkipView(SkipSystem.GetSkips());
        }
        
    }
}