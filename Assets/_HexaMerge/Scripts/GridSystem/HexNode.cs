using UnityEngine;

public class HexNode
{
    private Vector2 _position;
    private int _index;

    public HexNode(Vector2 position, int index)
    {
        _position = position;
        _index = index;
    }

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public int Index
    {
        get => _index;
        set => _index = value;
    }
}
