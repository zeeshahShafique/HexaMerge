using _Hexa_Merge.Scripts.Input.Interfaces;
using _HexaMerge.Scripts.Input.States;
using UnityEngine;

public class InputSystem : MonoBehaviour, IInputState
{
    private Vector2 _startPosition, _endPosition;
    private InputState _state;

    [SerializeField] private TileController TileController;

    // Start is called before the first frame update
    private void Start()
    {
        ChangeState(new IdleInputState(this, TileController, TileController));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _state.Begin(touch);
                break;
            case TouchPhase.Stationary:
            case TouchPhase.Moved:
                _state.Move(touch);
                break;
            case TouchPhase.Ended:
                _state.End(touch);
                break;
        }
    }

    public void ChangeState(InputState state)
    {
        _state = state;
    }
}
