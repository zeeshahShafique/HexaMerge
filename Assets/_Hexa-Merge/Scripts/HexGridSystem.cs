using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using UnityEngine;

public class HexGridSystem : MonoBehaviour
{
    [SerializeField] private int _GridSize;

    [SerializeField] private GameObject _HexNodePrefab;

    [SerializeField] private List<List<HexNode>> _nodes = new List<List<HexNode>>();

    [SerializeField] private GridSystemSO _grid;
    
    private Vector2 _position = Vector2.zero;
     // Start is called before the first frame update
    void Start()
    {
        _grid.Nodes = new List<GameObject>();
        _grid.NodesPositions = new List<Vector2>();
        GridLayoutGenerator();
        GenerateGrid();
    }

    private void GridLayoutGenerator()
    {
        int k = 0, size;
        int index = 0;
        for (int r = 0; r < _GridSize; r++)
        {
            size = (r % 2 == 1) ? _GridSize - 1 : _GridSize;
            _nodes.Add(new List<HexNode>());
            for (int i = 0; i < size; i++)
            {
                var pos = new Vector2(i + k, r);
                _nodes[r].Add(new HexNode(pos, index++));
            }

            if ((r + 1) % 2 == 0)
                k--;
        }
    }

    void GenerateGrid()
    {
        float x, y = 0;
        for (int i = 0; i < _nodes.Count; i++)
        {
            x = (i % 2 == 0) ? 0 : 0.5f;
            for (int n = 0; n < _nodes[i].Count; n++)
            {
                _position.Set(x, y);
                _grid.NodesPositions.Add(_position);
                var tile = Instantiate(_HexNodePrefab, _position, Quaternion.identity, this.transform);
                _grid.Nodes.Add(tile);
                x += 1;
            }

            y -= 0.866f;
        }
    }
}

public class HexNode
{
    private Vector2 _position;
    private int _index;

    public HexNode(Vector2 position, int index)
    {
        _position = position;
        _index = index;
    }
}
