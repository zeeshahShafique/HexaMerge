using System.Collections.Generic;
using UnityEngine;

public class HexGridSystem : MonoBehaviour
{
    [SerializeField] private int GridSize;

    [SerializeField] private GameObject HexNodePrefab;

    private List<List<HexNode>> _nodes = new();

    [SerializeField] private GridSystemSO Grid;
    
    private Vector2 _position = Vector2.zero;
     // Start is called before the first frame update
    void Start()
    {
        Grid.Nodes = new List<GameObject>();
        Grid.NodesPositions = new List<Vector2>();
        Grid.Vacant = new bool[23];
        GridLayoutGenerator();
        GenerateGrid();
        Grid.NodeInfo = _nodes;
    }

    private void GridLayoutGenerator()
    {
        int k = 0, size;
        int index = 0;
        for (int r = 0; r < GridSize; r++)
        {
            size = (r % 2 == 1) ? GridSize - 1 : GridSize;
            _nodes.Add(new List<HexNode>());
            for (int i = 0; i < size; i++)
            {
                _position.Set(i + k, r);
                _nodes[r].Add(new HexNode(_position, index++));
            }

            if ((r + 1) % 2 == 0)
                k--;
        }
    }

    private void GenerateGrid()
    {
        float x, y = 0;
        int index = 0;
        for (int i = 0; i < _nodes.Count; i++)
        {
            x = (i % 2 == 0) ? 0 : 0.5f;
            for (int n = 0; n < _nodes[i].Count; n++)
            {
                _position.Set(x, y);
                Grid.NodesPositions.Add(_position);
                var tile = Instantiate(HexNodePrefab, _position, Quaternion.identity, this.transform);
                Grid.Nodes.Add(tile.gameObject);
                Grid.Vacant[index++] = true;
                x += 1;
            }

            y -= 0.866f;
        }
    }
    
    #region Neighbors
    
        
        private Vector2 Add(Vector2 pos, Vector2 dir)
        {
            return pos + dir;
        }

        public Vector2 Neighbour(Vector2 pos, Vector2 direction)
        {
            return Add(pos, direction);
        }
        
    #endregion
}
