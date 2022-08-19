using _Hexa_Merge.Scripts.Input;
using _Hexa_Merge.Scripts.Input.Interfaces;
using UnityEngine;

public class InputSystem : MonoBehaviour, IInputState
{
    private Touch theTouch;
    private Vector2 startPosition, endPosition;
    private InputState _state;

    private IInputState _iState;
    
    // Start is called before the first frame update
    void Start()
    {
        _iState = GetComponent<IInputState>();
        ChangeState(new IdleInputState(_iState));
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        theTouch = Input.GetTouch(0);
        switch (theTouch.phase)
        {
            case TouchPhase.Began:
                _state.Begin();
                break;
            case TouchPhase.Moved:
                _state.Move();
                break;
            case TouchPhase.Ended:
                _state.End();
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
