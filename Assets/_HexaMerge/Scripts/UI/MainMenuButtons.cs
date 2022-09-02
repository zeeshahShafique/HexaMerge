using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Scrollbar Scrollbar;
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

    public void OnButtonPressed(float endValue)
    {
        _timeElapsed = 0;
        _endValue = endValue / 5f;
    }

}
