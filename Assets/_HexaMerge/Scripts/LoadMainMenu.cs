using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    [SerializeField] private Image FillBar;

    private Button _playButton;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        FillBar.fillAmount = 0;
        AsyncOperation menuLoad = SceneManager.LoadSceneAsync("MainMenu");
        while (!menuLoad.isDone)
        {
            FillBar.fillAmount = menuLoad.progress;
            yield return null;
        }
    }
}
