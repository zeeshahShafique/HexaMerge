using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;

public class HexGridSystem : MonoBehaviour
{
    [SerializeField] private int _GridSize;
    // Start is called before the first frame update
    void Start()
    {
        List<List<Vector2>> nodes = new List<List<Vector2>>();
        int k = 0, size = _GridSize;

        for (int r = 0; r < _GridSize; r++)
        {
            size = (r % 2 == 1) ? _GridSize - 1 : _GridSize;
            nodes.Add(new List<Vector2> ());
            for (int i = 0; i < size; i++)
            {
                nodes[r].Add(new Vector2(i+k, r));
            }
            if ((r + 1) % 2 == 0)
                k--;
        }

        int count = 0;
        for (int i = 0; i < nodes.Count; i++)
            for (int j = 0; j < nodes[i].Count; j++)
                Debug.Log($"Node {count++}: ({nodes[i][j].x} , {nodes[i][j].y})");
    }
}
