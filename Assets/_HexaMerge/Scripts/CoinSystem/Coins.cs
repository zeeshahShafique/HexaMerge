using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "ScriptableObject/CoinSystem", order = 1)]
public class Coins : ScriptableObject
{
    [SerializeField] private int CoinAmount = 500;
    [SerializeField] private String CoinPrefKey;

    private void OnEnable()
    {
        if(PlayerPrefs.HasKey(CoinPrefKey))
            CoinAmount = PlayerPrefs.GetInt(CoinPrefKey);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt(CoinPrefKey, CoinAmount);
        PlayerPrefs.Save();
    }

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
