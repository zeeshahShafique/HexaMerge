using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CoinSystem", order = 1)]
public class Coins : ScriptableObject
{
    [SerializeField] private int CoinAmount;

    public void AddCoins(int amount)
    {
        CoinAmount += amount;
    }

    public void RemoveCoins(int amount)
    {
        CoinAmount -= amount;
    }

    public int GetCoins()
    {
        return CoinAmount;
    }
}
