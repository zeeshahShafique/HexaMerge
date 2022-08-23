using System.Data.Common;
using _Hexa_Merge.Scripts.Input;
using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

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
            default:
                break;
        }
        // if (Input.touchCount > 0){
        //     theTouch = Input.GetTouch(0);
        //     if (theTouch.phase == TouchPhase.Began){
        //         startPosition = theTouch.position;
        //     }
        //     else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended){
        //         endPosition = theTouch.position;
        //         var x = startPosition.x - endPosition.x;
        //         var y = startPosition.y - endPosition.y;
        //         if ((Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0) && theTouch.phase == TouchPhase.Ended) {
        //             Debug.Log("Just Tapped");
        //         }
        //         else if (Mathf.Abs(x) > Mathf.Abs(y)){
        //             Debug.Log("Moved in x");
        //         }
        //         else {
        //             Debug.Log("Moved in y");
        //         }
        //     }
        // }
    }

    public void ChangeState(InputState state)
    {
        _state = state;
    }
}
