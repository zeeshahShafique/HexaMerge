using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Scrollbar Scrollbar;
    [SerializeField] private HorizontalScrollSnap Snap;
    [SerializeField] private float LerpDuration;

    private float _endValue = 0.5f;
    private float _timeElapsed;


    private void Update()
    {
        if (_timeElapsed < LerpDuration)
        {
            Scrollbar.value = Mathf.Lerp(Scrollbar.value, _endValue, _timeElapsed / LerpDuration);
            _timeElapsed += Time.deltaTime;
        }
    }

    public void OnButtonPressed(int endValue)
    {
        Snap.GoToScreen(endValue);
        ChangeSlider();
    }

    public void ChangeSlider()
    {
        _timeElapsed = 0;
        _endValue = Snap.CurrentPage / 4f;
    }

}
