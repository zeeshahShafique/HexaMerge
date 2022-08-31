using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/GridSystemSO", order = 1, fileName = "GridData")]
public class GridSystemSO : ScriptableObject
{
    public List<Vector2> NodesPositions; // Stores cell position in real world.
    public List<GameObject> Nodes; // Stores actual GameObjects.
    public Dictionary<Vector2, HexNode> NodeInfo; // Stores dictionary of cell information against real world position.
    public Dictionary<Vector2, Vector2> CellPositions;

    private void OnEnable()
    {
        NodesPositions = new List<Vector2>();
        Nodes = new List<GameObject>();
        NodeInfo = new Dictionary<Vector2, HexNode>();
        CellPositions = new Dictionary<Vector2, Vector2>();
    }

    public void Clear()
    {
        NodesPositions.Clear();
        Nodes.Clear();
        NodeInfo.Clear();
        CellPositions.Clear();
    }

    private void OnDisable()
    {
        Clear();
    }

    public Vector2 WorldToCellPosition(Vector2 worldPosition)
    {
        return NodeInfo[worldPosition].Position;
    }

    public Vector2 IndexToWorldPosition(int index)
    {
        return NodesPositions[index];
    }
    
    public Vector2 CellToWorldPosition(Vector2 cellPosition)
    {
        return CellPositions[cellPosition];
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
