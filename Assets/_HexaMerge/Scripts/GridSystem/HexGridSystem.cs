using UnityEngine;

public class HexGridSystem : MonoBehaviour
{

    [SerializeField] private int GridSize;

    [SerializeField] private GameObject HexNodePrefab;

    [SerializeField] private GridSystemSO Grid;
    
    private Vector2 _position = Vector2.zero;
    private Vector2 _cellPosition = Vector2.zero;
    

    void Start()
    {
        GridLayoutGenerator();
    }

    private void GridLayoutGenerator()
    {
        int k = 0;
        int index = 0;
        float x, y = 0;
        for (int r = 0; r < GridSize; r++)
        {
            x = (r % 2 == 0) ? 0 : 0.5f;
            var size = (r % 2 == 1) ? GridSize - 1 : GridSize;
            for (int i = 0; i < size; i++)
            {
                _cellPosition.Set(i + k, r);
                var node = new HexNode(_cellPosition, index++, false);
                _position.Set(x, y);
                Grid.NodesPositions.Add(_position);
                var tile = Instantiate(HexNodePrefab, _position, Quaternion.identity, transform);
                Grid.Nodes.Add(tile.gameObject);
                Grid.NodeInfo.Add(_position, node);
                Grid.CellPositions.Add(_cellPosition, _position);
                x += 1;
            }
            y -= 0.866f;
            if ((r + 1) % 2 == 0)
                k--;
        }
    }
    
    
}
