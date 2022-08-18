using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    // private Touch theTouch;
    // private Vector2 startPosition, endPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
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
}
