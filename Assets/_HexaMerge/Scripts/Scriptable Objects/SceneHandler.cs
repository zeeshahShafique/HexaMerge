using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private Image FillBar;

    private Button _playButton;
    
    // Start is called before the first frame update
    void Start()
    {
        FillBar.fillAmount = 0;
        LoadGame();
    }

    IEnumerator LoadSceneAsync(String scene, bool allowActive = true)
    {
        AsyncOperation menuLoad = SceneManager.LoadSceneAsync(scene);
        menuLoad.allowSceneActivation = allowActive;
        while (!menuLoad.isDone)
        {
            FillBar.fillAmount = menuLoad.progress;
            yield return null;
        }
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsync("GameScene"));
    }
}
