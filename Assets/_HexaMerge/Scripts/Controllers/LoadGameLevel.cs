using System.Collections;
using _HexaMerge.Scripts.DynamicFeedback;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameLevel : MonoBehaviour
{
    private bool _playPressed;
    [SerializeField] private EnergySystem EnergySystem;

    [SerializeField] private DynamicFeedbackSO DynamicFeedback;

    [SerializeField] private DynamicOverlaySO DynamicOverlay;

    [SerializeField] private Button PlayButton;

    private void Start()
    {
        _playPressed = false;
        StartCoroutine(LoadGameSceneAsync());
    }

    private void OnEnable()
    {
        PlayButton.onClick.AddListener(LoadGame);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(LoadGame);
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
        PlayButton.gameObject.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f, 2, 1f).OnComplete(() =>
        {
            DynamicFeedback.PlayAudioSource(DynamicAudio.ButtonClick);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.SoftImpact);
            if (EnergySystem.HasLives())
            {
                EnergySystem.ReduceLives(1);
                _playPressed = true;
                return;
            }
            DynamicOverlay.EnableClickableOverlay("No Lives Available");
        });
        // else
        // {
        //     LivesOverlay.DOScale(Vector3.one, 0.2f).OnComplete(() =>
        //     {
        //         LivesOverlay.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutQuad).SetDelay(1.5f);
        //     }).SetEase(Ease.InQuad);
        // }
    }
}
