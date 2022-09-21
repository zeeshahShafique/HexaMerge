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
    
    private GameObject _tile;
    
    [SerializeField] private Vector3 CacheTilePos;

    [SerializeField] private GameObject[] TilePrefab;

    [SerializeField] private GridSystemSO Grid;

    [SerializeField] private Sprite HighlightSprite;

    [SerializeField] private Sprite CellSprite;

    [SerializeField] private MergeSystem MergeSystem;
    

    // Cache objects bellow.

    private readonly Color _color = new(0.1764f, 0.1764f, 0.1764f, 0.75f);
    
    private Camera _camera;
    private Vector2?[] _highlightVec, _highlightVecCache;
    
    private void OnEnable()
    {
        Grid.Clear();
        SkipSpawnedTile.OnSkip += SkipTile;
    }
    private void Start()
    {
        _camera = Camera.main;
        SpawnNewTile();
        CacheTilePos = _tile.transform.localPosition;
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
        var localPosition = _tile.transform.localPosition;
        float distance = Vector2.Distance(localPosition, movement);
        localPosition = Vector3.Lerp(localPosition,
            movement + Vector2.up,
            Time.deltaTime * 2 + distance);
        _tile.transform.localPosition = localPosition;
        HighlightCell();
    }
    private void HighlightCell()
    {
        var allTrue = true;
        for (int i = 0; i < _tile.transform.childCount; i++)
        {
            _highlightVec[i] = HighlightInGrid(_tile.transform.GetChild(i).position, Color.white);
            if (_highlightVec[i].HasValue && _highlightVecCache[i].Equals(_highlightVec[i])) continue;
            allTrue = false;
            _highlightVecCache[i] = _highlightVec[i];
            break;
        }
        if (allTrue) return;
        ClearGridHighlight();
    }
    private Vector2? HighlightInGrid(Vector2 objectTransform, Color color)
    {
        foreach (var key in Grid.NodeInfo.Keys)
        {
            Vector2 position = objectTransform;
            if (!(Mathf.Abs(position.x - key.x) < 0.5f) || 
                !(Mathf.Abs(position.y - key.y) < 0.433f)) continue;
            if (Grid.NodeInfo[key].Occupied) continue;
            HighlightNode(Grid.NodeInfo[key].Index, HighlightSprite, color);
            return key;
        }
        return null;
    }
    private void HighlightNode(int index, Sprite sprite, Color color)
    {
        var spriteRenderer = Grid.Nodes[index].GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
        spriteRenderer.sprite = sprite;
    }
    private void ClearGridHighlight()
    {
        for (int i = 0; i < Grid.NodesPositions.Count; i++)
        {
            HighlightNode(i, CellSprite, _color);
        }
    }
    
    public void OnDragEnd()
    {
        if (SnapTiles())
        {
            return;
        }
        Return(_tile);
    }
    private bool SnapTiles()
    {
        var allTrue = true;
        var indices = new Vector2?[_tile.transform.childCount];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = SnapOnGrid(_tile.transform.GetChild(i));
            if (!indices[i].HasValue)
                allTrue = false;
        }

        if (allTrue)
        {
            var sprites = new Sprite[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                Grid.NodeInfo[(Vector2) indices[i]].Occupied = true;
                var spriteRenderer = _tile.transform.GetComponentInChildren<TileInfo>().Sprite;
                Grid.NodeInfo[(Vector2)indices[i]].Sprite = spriteRenderer;
                sprites[i] = spriteRenderer.sprite;
                spriteRenderer.sortingOrder = 1;
                _tile.transform.GetChild(0).SetParent(Grid.Nodes[Grid.NodeInfo[(Vector2)indices[i]].Index].transform);
                Grid.OccupiedCount++;
            }

            for (int i = 0; i < indices.Length; i++)
            {
                MergeSystem.SearchTiles((Vector2)indices[i], sprites[i]);
            }

            SpawnNewTile();
            ClearGridHighlight();
            return true;
        }

        return false;
    }
    private Vector2? SnapOnGrid(Transform objectTransform)
    {
        foreach (var key in Grid.NodeInfo.Keys)
        {
            Vector2 position = objectTransform.position;
            if (!(Mathf.Abs(position.x - key.x) < 0.5f) || !(Mathf.Abs(position.y - key.y) < 0.433f)) continue;
            if (Grid.NodeInfo[key].Occupied) continue;
            Snap(objectTransform, key);
            return key;
        }
        return null;
    }
    private void Snap(Transform objectTransform,Vector2 snapPosition)
    {
        objectTransform.position = snapPosition;
    }
    private void SkipTile()
    {
        SpawnNewTile();
    }
    private void SpawnNewTile()
    {
        if(_tile)
            Destroy(_tile);
        var range = new Random();
        _tile = Instantiate(TilePrefab[range.Next(TilePrefab.Length)], CacheTilePos, Quaternion.identity, this.transform);
    }
    private void Return(GameObject tile)
    {
        var distance = Vector2.Distance(CacheTilePos, tile.transform.position);
        tile.transform.DOMove(CacheTilePos, distance * Time.deltaTime).SetEase(Ease.Linear);
        if (_tile.transform.CompareTag("CombinedTile"))
        {
            var pos = 0.5f;
            for (int i = 0; i < _tile.transform.childCount; i++)
            {
                tile.transform.GetChild(i).localPosition = Vector3.left * pos;
                pos = -pos;
            }
        }
    }
    
    public void OnTap()
    {
        if (_tile.transform.CompareTag("CombinedTile"))
        {
            _tile.transform.Rotate(Vector3.forward, 60, Space.Self);
            for (int i = 0; i < _tile.transform.childCount; i++)
            {
                _tile.transform.GetChild(i).Rotate(Vector3.back, 60, Space.Self);
            }
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
