using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    private void OnEnable()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public SpriteRenderer Sprite
    {
        get => _sprite;
        set => _sprite = value;
    }
}
