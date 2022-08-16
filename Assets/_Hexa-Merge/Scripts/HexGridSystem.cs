using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class HexGridSystem : MonoBehaviour
{
    [SerializeField] private int _GridSize;

    [SerializeField] private GameObject _HexNodePrefab;
    
    private List<List<Vector2>> nodes = new List<List<Vector2>>();
    
    private Vector2 position = Vector2.zero;
     // Start is called before the first frame update
    void Start()
    {
        GridLayoutGenerator();
        GenerateGrid();
    }

    private void GridLayoutGenerator()
    {
        int k = 0, size = _GridSize;

        for (int r = 0; r < _GridSize; r++)
        {
            size = (r % 2 == 1) ? _GridSize - 1 : _GridSize;
            nodes.Add(new List<Vector2>());
            for (int i = 0; i < size; i++)
            {
                nodes[r].Add(new Vector2(i + k, r));
            }

            if ((r + 1) % 2 == 0)
                k--;
        }
    }

    void GenerateGrid()
    {
        float x = 0, y = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            x = (i % 2 == 0) ? 0 : 0.5f;
            for (int n = 0; n < nodes[i].Count; n++)
            {
                position.Set(x, y);
                Instantiate(_HexNodePrefab, position, Quaternion.identity, this.transform);
                x += 1;
            }
            y -= 0.866f;
        }
    }
}
