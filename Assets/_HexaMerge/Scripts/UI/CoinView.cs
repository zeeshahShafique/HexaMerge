using TMPro;
using UnityEngine;

public class CoinView : MonoBehaviour
{
    [SerializeField] private Coins Coins;
    [SerializeField] private TextMeshProUGUI CoinText;
    
    private void Awake()
    {
        ChangeCoinView(Coins.GetCoins());
    }

    private void OnEnable()
    {
        Coins.ChangeCoinText += ChangeCoinView;
    }

    private void OnDisable()
    {
        Coins.ChangeCoinText -= ChangeCoinView;
    }

    private void ChangeCoinView(int amount)
    {
        CoinText.text = amount.ToString();
    }

}
