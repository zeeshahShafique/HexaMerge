using System.Collections.Generic;
using UnityEngine;

public class HexGridSystem : MonoBehaviour
{
    [SerializeField] private int _GridSize;

    [SerializeField] private GameObject _HexNodePrefab;
    
    private List<List<Node>> _nodes = new();
    
    private Vector2 _position = Vector2.zero;
     // Start is called before the first frame update
    void Start()
    {
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
            _nodes.Add(new List<Node>());
            for (int i = 0; i < size; i++)
            {
                var pos = new Vector2(i + k, r);
                _nodes[r].Add(new Node(pos, index++));
            }

            if ((r + 1) % 2 == 0)
                k--;
        }
    }

    void GenerateGrid()
    {
        float x = 0, y = 0;
        for (int i = 0; i < _nodes.Count; i++)
        {
            x = (i % 2 == 0) ? 0 : 0.5f;
            for (int n = 0; n < _nodes[i].Count; n++)
            {
                _position.Set(x, y);
                Instantiate(_HexNodePrefab, _position, Quaternion.identity, this.transform);
                x += 1;
            }

            y -= 0.866f;
        }
    }
}