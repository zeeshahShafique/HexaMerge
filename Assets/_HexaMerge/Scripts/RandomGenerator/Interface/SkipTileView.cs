using TMPro;
using UnityEngine;

namespace _HexaMerge.Scripts.RandomGenerator.Interface
{
    public class SkipTileView: MonoBehaviour
    {
        [SerializeField] private SkipSpawnedTile SkipSystem;
        [SerializeField] private TextMeshProUGUI SkipText;

        private void Update()
        {
            if (SkipSystem.GetSkips().ToString() != SkipText.text)
            {
                SkipText.text = SkipSystem.GetSkips().ToString();
            }
        }
    }
}