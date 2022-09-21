using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CoinSystem/CoinSO", order = 1)]
public class Coins : ScriptableObject
{
    [SerializeField] private int CoinAmount = 500;
    [SerializeField] private String CoinPrefKey;

    public Action<int> ChangeCoinText;

    private void OnEnable()
    {
        if(PlayerPrefs.HasKey(CoinPrefKey))
            CoinAmount = PlayerPrefs.GetInt(CoinPrefKey);
    }
    
    private void SavePlayerPref()
    {
        PlayerPrefs.SetInt(CoinPrefKey, CoinAmount);
        PlayerPrefs.Save();
    }

    public void AddCoins(int amount)
    {
        CoinAmount += amount;
        SavePlayerPref();
        ChangeCoinText?.Invoke(CoinAmount);
    }

    public void RemoveCoins(int amount)
    {
        CoinAmount -= amount;
        SavePlayerPref();
        ChangeCoinText?.Invoke(CoinAmount);
    }

    public int GetCoins()
    {
        return CoinAmount;
    }
}
