using UnityEngine;

public class TileInfo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _Sprite;

    private void OnEnable()
    {
        _Sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public SpriteRenderer Sprite
    {
        get => _Sprite;
        set => _Sprite = value;
    }
}
