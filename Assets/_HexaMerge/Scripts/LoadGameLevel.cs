using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameLevel : MonoBehaviour
{
    private bool _playPressed;

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
        _playPressed = true;
    }
}
