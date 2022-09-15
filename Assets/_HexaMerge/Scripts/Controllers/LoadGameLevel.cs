using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameLevel : MonoBehaviour
{
    private bool _playPressed;
    [SerializeField] private LivesSystem LivesSystem;

    [SerializeField] private RectTransform LivesOverlay;

    private void Start()
    {
        _playPressed = false;
        StartCoroutine(LoadGameSceneAsync());
    }

    IEnumerator LoadGameSceneAsync(bool allowActive = false)
    {
        AsyncOperation menuLoad = SceneManager.LoadSceneAsync("GameLevel");
        menuLoad.allowSceneActivation = allowActive;
        while (!menuLoad.isDone)
        {
            if (_playPressed)
                menuLoad.allowSceneActivation = true;
            yield return null;
        }
    }

    public void LoadGame()
    {
        if (LivesSystem.HasLives())
        {
            LivesSystem.ReduceLives(1);
            _playPressed = true;
        }
        // else
        // {
        //     LivesOverlay.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        //     {
        //         LivesOverlay.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad).SetDelay(1.5f);
        //     }).SetEase(Ease.InQuad);
        // }
    }
}
