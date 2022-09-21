using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private Image FillBar;

    private Button _playButton;

    public float LoadTimer;

    [SerializeField] private AdSystem AdSystem;
    [SerializeField] private StoreSO Store;
    
    // Start is called before the first frame update
    void Start()
    {
        AdSystem.AdSystemInit();
        Store.Initialize();
        LoadTimer = 5;
        StartCoroutine(LoadSceneAsync());
    }

    private void Update()
    {
        if (LoadTimer > 0)
            LoadTimer -= Time.deltaTime;
    }

    IEnumerator LoadSceneAsync()
    {
        FillBar.fillAmount = 0;
        AsyncOperation menuLoad = SceneManager.LoadSceneAsync("MainMenu");
        menuLoad.allowSceneActivation = false;
        while (!menuLoad.isDone)
        {
            FillBar.fillAmount = menuLoad.progress;
            if ((AdSystem.IsAdSystemInitialized() && Store.IsInitialized) || LoadTimer < 1)
                menuLoad.allowSceneActivation = true;
            yield return null;
        }
    }
}
