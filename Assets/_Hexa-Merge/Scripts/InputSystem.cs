using System;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    private Touch theTouch;
    private Vector2 startPosition, endPosition, touchToWorldPos;

    private Camera _camera;
    private RaycastHit2D _hitInfo;
    private GameObject _tile;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount <= 0) return;
        theTouch = Input.GetTouch(0);
        if (theTouch.phase == TouchPhase.Began){
            startPosition = theTouch.position;
            touchToWorldPos = _camera.ScreenToWorldPoint(startPosition);
            _hitInfo = Physics2D.Raycast(touchToWorldPos, _camera.transform.forward, Mathf.Infinity);
            if(!_hitInfo.collider) return;
        }
        if (!_hitInfo.collider.CompareTag("Tile")) return;
        _tile = _hitInfo.transform.gameObject;
        if (theTouch.phase != TouchPhase.Moved && theTouch.phase != TouchPhase.Ended) return;
        endPosition = theTouch.position;
        var x = startPosition.x - endPosition.x;
        var y = startPosition.y - endPosition.y;
        if ((Mathf.Abs(x) == 0 && Mathf.Abs(y) == 0) && theTouch.phase == TouchPhase.Ended)
        {
            Debug.Log("Just Tapped");
            // _tile.transform.localEulerAngles += Vector3.forward * 60;
        }
        else if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            Debug.Log("Moved in x");
        }
        else
        {
            Debug.Log("Moved in y");
        }
    }
}
