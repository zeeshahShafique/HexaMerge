using System;
using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [SerializeField] private Coins Coins;
    [SerializeField] private TextMeshProUGUI CoinText;

    private int _currentCoins;

    private void Start()
    {
        _currentCoins = Int32.Parse(CoinText.text);
    }

    private void Update()
    {
        if (Coins.GetCoins() != _currentCoins)
        {
            _currentCoins = Coins.GetCoins();
            CoinText.text = _currentCoins.ToString();
        }
    }
}
