using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/TierConfig", order = 1, fileName = "TileTierConfig")]
public class TileTierConfig : ScriptableObject
{
    public SerializableDictionaryBase<Sprite, Sprite> Tiles;
}
