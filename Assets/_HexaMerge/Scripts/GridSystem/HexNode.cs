using UnityEngine;

public class HexNode
{
    private Vector2 _position;
    private int _index;
    private bool _occupied;
    private SpriteRenderer _sprite;

    public HexNode(Vector2 position, int index, bool occupied)
    {
        _position = position;
        _index = index;
        _occupied = occupied;
        _sprite = null;
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

    public bool Occupied
    {
        get => _occupied;
        set => _occupied = value;
    }

    public SpriteRenderer Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
}
