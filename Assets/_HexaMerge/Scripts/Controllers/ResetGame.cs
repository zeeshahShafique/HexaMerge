using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GridSystemSO Grid;
    [SerializeField] private AdSystem AdSystem;
    
    [SerializeField] private float ExitPanelTweenDuration;
    [SerializeField] private Ease ExitPanelEase;

    [SerializeField] private RectTransform ExitPanelRect;
    public void ResetLevel()
    {
        Grid.Clear();
        SceneManager.LoadScene("GameLevel");
    }

    public void ExitLevel(bool showAd)
    {
        Grid.Clear();
        if (showAd)
            AdSystem.ShowInterstitialsAd();
        Invoke(nameof(LoadMainMenu), 0.2f);
    }

    public void ShowExitPanel()
    {
        ExitPanelRect.gameObject.SetActive(true);
        ExitPanelRect.GetChild(0).DOScale(Vector3.one, ExitPanelTweenDuration).SetEase(ExitPanelEase);
    }

    public void HideExitPanel()
    {
        ExitPanelRect.GetChild(0).DOScale(Vector3.zero, ExitPanelTweenDuration).SetEase(ExitPanelEase).OnComplete((() =>
        {
            ExitPanelRect.gameObject.SetActive(false);
        }));
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
