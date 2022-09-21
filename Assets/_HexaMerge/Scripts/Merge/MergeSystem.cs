using System.Collections.Generic;
using _HexaMerge.Scripts.DynamicFeedback;
using _HexaMerge.Scripts.Enums;
using _HexaMerge.Scripts.MergeSystem.MergeStateMachine.States;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/MergeSystem", order = 1, fileName = "MergeSystem")]
public class MergeSystem : ScriptableObject
{
    private MergeState _mergeState;

    [SerializeField] private GridSystemSO Grid;

    [SerializeField] private Sprite CellSprite;

    [SerializeField] private TileTierConfig TierConfig;

    [SerializeField] private DynamicFeedbackSO DynamicFeedback;
    
    private List<int> _checkList = new();

    private Sprite _sprite;

    private List<int> _matches = new();

    private Queue<int> _queue = new();
    
    private readonly Color _color = new(0.1764f, 0.1764f, 0.1764f, 0.75f);

    private readonly Vector2[] _directions = { Direction.TopLeft, Direction.Left, Direction.BottomLeft,
        Direction.BottomRight, Direction.Right, Direction.TopRight };


    public void SearchTiles(Vector2 tile, Sprite sprite)
    {
        _checkList.Clear();
        _sprite = sprite;
        _matches.Clear();
        SearchTiles(tile);
        if (_matches.Count > 2)
        {
            MergeTiles(tile);
            DynamicFeedback.PlayAudioSource(DynamicAudio.Match3);
            DynamicFeedback.PlayHapticsSource(DynamicHaptics.Success);
            SearchTiles(tile, _sprite);
        }
    }

    private void SearchTiles(Vector2 tile)
    {
        if (!Grid.NodeInfo.ContainsKey(tile)) return;
        _queue.Enqueue(Grid.NodeInfo[tile].Index);
        _matches.Add(Grid.NodeInfo[tile].Index);
        if (_queue.Count < 1)
            return;
        _checkList.Add(_queue.Dequeue());
        for (int i = 0; i < 6; i++)
        {
            var neighbour = Grid.Neighbour(Grid.WorldToCellPosition(tile), _directions[i]);
            if (Grid.CellPositions.ContainsKey(neighbour))
            {
                neighbour = Grid.CellToWorldPosition(neighbour);
                if (_checkList.Contains(Grid.NodeInfo[neighbour].Index)) continue;
                if (Grid.NodeInfo[neighbour].Occupied &&
                    Grid.NodeInfo[neighbour].Sprite.sprite == _sprite)
                {
                    SearchTiles(neighbour);
                }
                else
                {
                    _checkList.Add(Grid.NodeInfo[neighbour].Index);
                }
            }
        }
    }
    
    private void MergeTiles(Vector2 tile)
    {
        for (int i = 0; i < _matches.Count; i++)
        {
            var node = Grid.NodeInfo[Grid.IndexToWorldPosition(_matches[i])];
            var spriteRenderer = node.Sprite; 
            if (Grid.NodeInfo[tile].Index == _matches[i])
            {
                spriteRenderer.sprite = TierConfig.Tiles[spriteRenderer.sprite];
                _sprite = spriteRenderer.sprite;
                continue;
            }
            node.Occupied = false;
            spriteRenderer.sprite = CellSprite;
            spriteRenderer.color = _color;
            
            Destroy(Grid.Nodes[_matches[i]].transform.GetChild(0).gameObject);
            Grid.OccupiedCount--;
        }
    }
}
