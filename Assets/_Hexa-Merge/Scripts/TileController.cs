using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour, IDrag, ITap
{
    [SerializeField] private GameObject _Tile;

    [SerializeField] private GameObject _TilePrefab;
    
    private Camera _camera;

    private Vector2 _cacheTilePos;
    private void Start()
    {
        _camera = Camera.main;
        _cacheTilePos = _Tile.transform.localPosition;
    }

    public void OnDrag(Touch touch)
    {
        Vector2 movement = _camera.ScreenToWorldPoint(touch.position);
        var localPosition = _Tile.transform.localPosition;
        float distance = Vector2.Distance(localPosition, movement);
        localPosition = Vector3.Lerp(localPosition,
            movement + Vector2.up,
            Time.deltaTime * 2 + distance);
        _Tile.transform.localPosition = localPosition;
        // Debug.Log("OnDrag Called");
        // throw new System.NotImplementedException();
    }

    public void OnDragEnd()
    {
        var x = Mathf.Abs(_Tile.transform.localPosition.magnitude - Vector2.zero.magnitude);
        Debug.Log(x);
        if (x <= 0.5f)
            Snap();
        else
            Return();
        // Debug.Log("OnDragEnd Called");
        // throw new System.NotImplementedException();
    }

    private void Snap()
    {
        _Tile.transform.localPosition = Vector3.zero;
        _Tile = Instantiate(_TilePrefab, _cacheTilePos, Quaternion.identity);
    }

    private void Return()
    {
        _Tile.transform.localPosition = _cacheTilePos;
    }

    public void OnTap()
    {
        _Tile.transform.Rotate(Vector3.forward, 60, Space.Self);
        Debug.Log("OnTap Called");
        // throw new System.NotImplementedException();
    }
}
