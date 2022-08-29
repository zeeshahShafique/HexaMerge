using System;
using _HexaMerge.Scripts.NodeStateMachine.Interface;
using _HexaMerge.Scripts.NodeStateMachine.States;
using _HexaMerge.Scripts.RandomGenerator;
using DG.Tweening;
using UnityEngine;
using Random = System.Random;

public class TileController : MonoBehaviour, IDrag, ITap, INodeState

{
#region OldCode
    
    [SerializeField] private GameObject _Tile;
    
    [SerializeField] private GameObject[] _TilePrefab;
    
    [SerializeField] private GridSystemSO _Grid;

    
    // Cache objects bellow.
    
    private readonly Color _color = new(0.1764f, 0.1764f, 0.1764f, 0.75f);
    
    private Camera _camera;
    
    private Vector3 _cacheTilePos;

    private Vector2?[] _highlightVec, _highlightVecCache;

    private void OnEnable()
    {
        SkipSpawnedTile.OnSkip += SkipTile;
    }

    private void Start()
    {
        _camera = Camera.main;
        _cacheTilePos = _Tile.transform.localPosition;
        _highlightVec = new Vector2?[2];
        _highlightVecCache = new Vector2?[2];
    }

    private void OnDisable()
    {
        SkipSpawnedTile.OnSkip -= SkipTile;
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
            _highlightVec[0] = HighlightInGrid(_Tile.transform.GetChild(0).position, Color.green);
            _highlightVec[1] = HighlightInGrid(_Tile.transform.GetChild(1).position, Color.green);
            if (!_highlightVec[0].HasValue || !_highlightVec[1].HasValue || !_highlightVecCache[0].Equals(_highlightVec[0]) || !_highlightVecCache[1].Equals(_highlightVec[1]))
            {
                _highlightVecCache[0] = _highlightVec[0];
                _highlightVecCache[1] = _highlightVec[1];
                ClearGridHighlight();
            }
        }
        else
        {
            _highlightVec[0] = HighlightInGrid(_Tile.transform.position, Color.green);
            if (!_highlightVec[0].HasValue || !_highlightVecCache[0].Equals(_highlightVec[0]))
            {
                _highlightVecCache[0] = _highlightVec[0];
                ClearGridHighlight();
            }
        }
    }

    private Vector2? HighlightInGrid(Vector2 objectTransform, Color color)
    {
        for (var i = 0; i < _Grid.NodesPositions.Count; i++)
        {
            var localPosition = objectTransform;
            var x = localPosition.x;
            var y = localPosition.y;
            if (_Grid.Vacant[i] && (Math.Abs(x - _Grid.NodesPositions[i].x) < 0.5f) &&
                Math.Abs(y - _Grid.NodesPositions[i].y) < 0.433f)
            {
                HighlightNode(i, color);
                return _Grid.NodesPositions[i];
            }
        }
        return null;
    }
    
    private void HighlightNode(int index, Color color)
    {
        _Grid.Nodes[index].GetComponent<SpriteRenderer>().color = color;
    }
    
    private void ClearGridHighlight()
    {
        for (int i = 0; i < _Grid.NodesPositions.Count; i++)
        {
            HighlightNode(i, _color);
        }
    }
    
    public void OnDragEnd()
    {
        if (_Tile.transform.CompareTag("CombinedTile"))
        {
            var index1 = SnapOnGrid(_Tile.transform.GetChild(0));
            var index2 = SnapOnGrid(_Tile.transform.GetChild(1));
            if (index1 != -1 && index2 != -1)
            {
                _Grid.Vacant[index1] = false;
                _Grid.Vacant[index2] = false;
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
            var index = SnapOnGrid(_Tile.transform); 
            if (index != -1)
            {
                _Grid.Vacant[index] = false;
                _Tile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                SpawnNewTile();
                return;
            }
        }
        Return(_Tile);
    }

    private void SkipTile()
    {
        var OldTile = _Tile;
        SpawnNewTile();
        Destroy(OldTile);
    }
    private void SpawnNewTile()
    {
        var range = new Random();
        _Tile = Instantiate(_TilePrefab[range.Next(_TilePrefab.Length)], _cacheTilePos, Quaternion.identity);
    }
    
    private int SnapOnGrid(Transform objectTransform)
    {
        for (var i = 0; i < _Grid.NodesPositions.Count; i++)
        {
            var localPosition = objectTransform.position;
            var x = localPosition.x;
            var y = localPosition.y;
            if (_Grid.Vacant[i] && (Math.Abs(x - _Grid.NodesPositions[i].x) < 0.5f) &&
                Math.Abs(y - _Grid.NodesPositions[i].y) < 0.433f)
            {
                Snap(objectTransform, _Grid.NodesPositions[i]);
                 return i;
            }
        }
        return -1;
    }
  
    private void Snap(Transform objectTransform,Vector2 snapPosition)
    {
        objectTransform.position = snapPosition;
    }
    
    private void Return(GameObject tile)
    {
        tile.transform.DOMove(_cacheTilePos, 0.5f).SetEase(Ease.Linear);
        if (_Tile.transform.CompareTag("CombinedTile"))
        {
            tile.transform.GetChild(0).localPosition = Vector3.left * 0.5f;
            tile.transform.GetChild(1).localPosition = Vector3.right * 0.5f;
        }
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
    
#endregion

#region NewCode

    private NodeState _nodeState;
    public void ChangeNodeState(NodeState nodeState)
    {
        _nodeState = nodeState;
    }
    
    // public void OnDrag(Touch touch)
    // {
    //     _nodeState.Begin();
    // }
    //
    // public void OnDragEnd()
    // {
    //     _nodeState.Return();
    // }
    //
    // public void OnTap()
    // {
    //     // Rotate tile in tray here.
    // }
    
#endregion

}
