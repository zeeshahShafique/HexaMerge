using DG.Tweening;
using UnityEngine;

public class LevelFailed : MonoBehaviour
{
    [SerializeField] private RectTransform LevelFailPanel;
    [SerializeField] private float LevelFailPanelTweenDuration;
    [SerializeField] private Ease LevelFailPanelEase;

    [SerializeField] private GameObject GameMenuButtons;

    [SerializeField] private GridSystemSO Grid;
    [SerializeField] private AdSystem AdSystem;

    private bool _isActive = false;

    private void Start()
    {
        _isActive = false;
    }

    private void Update()
    {
        if (Grid.OccupiedCount > 1 && Grid.OccupiedCount >= Grid.Nodes.Count && !_isActive)
        {
            ShowPanel();
            Debug.LogError($"Level Failed");
            _isActive = true;
        }
    }

    public void ShowPanel()
    {
        LevelFailPanel.gameObject.SetActive(true);
        LevelFailPanel.DOScale(Vector3.one, LevelFailPanelTweenDuration).SetEase(LevelFailPanelEase);
        for (int i = 0; i < GameMenuButtons.transform.childCount; i++)
        {
            GameMenuButtons.transform.GetChild(i).gameObject.SetActive(false);
        }
        AdSystem.ShowInterstitialsAd();
    }
}
