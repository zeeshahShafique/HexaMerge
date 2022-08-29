using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObject/GridSystemSO", order = 1, fileName = "GridData")]
public class GridSystemSO : ScriptableObject
{
    public List<Vector2> NodesPositions;
    public List<GameObject> Nodes;
    public bool[] Vacant;
    public List<List<HexNode>> NodeInfo;
}
