using System;
using UnityEngine;
using Random = System.Random;

public class TileController : MonoBehaviour, IDrag, ITap
{
    [SerializeField] private GameObject _Tile;

    [SerializeField] private GameObject[] _TilePrefab;

    [SerializeField] private GridSystemSO _Grid;
    
    private Camera _camera;

    private Vector3 _cacheTilePos;
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
        if (_Tile.transform.childCount > 0)
        {
            HighlightInGrid(_Tile.transform.GetChild(0));
            HighlightInGrid(_Tile.transform.GetChild(1));
        }
        else
        {
            HighlightInGrid(_Tile.transform); 
        }
    }
    
    public void OnDragEnd()
    {
        if (_Tile.transform.CompareTag("CombinedTile"))
        {
            if (SnapOnGrid(_Tile.transform.GetChild(0)) && SnapOnGrid(_Tile.transform.GetChild(1)))
            {
                foreach (var spriteRenderer in _Tile.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteRenderer.sortingOrder = 1;
                }
                SpawnNewTile();
                return;
            }
        }
        else if (_Tile.transform.CompareTag("Tile"))
        {
            if (SnapOnGrid(_Tile.transform))
            {
                _Tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                SpawnNewTile();
                return;
            }
        }
        Return(_Tile.transform);
    }

    private void SpawnNewTile()
    {
        var range = new Random();
        _Tile = Instantiate(_TilePrefab[range.Next(_TilePrefab.Length)], _cacheTilePos, Quaternion.identity);
    }

    private bool SnapOnGrid(Transform objectTransform)
    {
        for (var i = 0; i < _Grid.NodesPositions.Count; i++)
        {
            var localPosition = objectTransform.position;
            var x = localPosition.x;
            var y = localPosition.y;
            if ((Math.Abs(x - _Grid.NodesPositions[i].x) < 0.5f) &&
                Math.Abs(y - _Grid.NodesPositions[i].y) < 0.433f)
            {
                Snap(objectTransform, _Grid.NodesPositions[i]);
                return true;
            }
        }
        return false;
    }

    private void HighlightInGrid(Transform objectTransform)
    {
        for (var i = 0; i < _Grid.NodesPositions.Count; i++)
        {
            var localPosition = objectTransform.position;
            var x = localPosition.x;
            var y = localPosition.y;
            if ((Math.Abs(x - _Grid.NodesPositions[i].x) < 0.5f) &&
                Math.Abs(y - _Grid.NodesPositions[i].y) < 0.433f)
            {
                HighlightNode(i, Color.green);
            }
            else
            {
                HighlightNode(i, Color.white);
            }
        }
    }
    
    private void HighlightNode(int index, Color color)
    {
        _Grid.Nodes[index].GetComponent<SpriteRenderer>().color = color;
    }

    private void Snap(Transform objectTransform,Vector2 snapPosition)
    {
        objectTransform.position = snapPosition;
    }

    private void Return(Transform objectTransform)
    {
        objectTransform.position = _cacheTilePos;
    }

    public void OnTap()
    {
        if (_Tile.transform.CompareTag("CombinedTile"))
        {
            _Tile.transform.Rotate(Vector3.forward, 60, Space.Self);
            _Tile.transform.GetChild(0).Rotate(Vector3.back, 60, Space.Self);
            _Tile.transform.GetChild(1).Rotate(Vector3.back, 60, Space.Self);
        }
    }
}
