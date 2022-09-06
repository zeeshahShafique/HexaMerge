using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    [SerializeField] private GridSystemSO Grid;
    [SerializeField] private AdSystem AdSystem;
    public void ResetLevel()
    {
        Grid.Clear();
        SceneManager.LoadScene("GameLevel");
    }

    public void ExitLevel()
    {
        AdSystem.ShowInterstitialsAd();
        Invoke(nameof(LoadMainMenu), 0.2f);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
