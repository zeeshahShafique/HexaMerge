using _HexaMerge.Scripts.Shop.Enum;
using _HexaMerge.Scripts.UI.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BigCardView : CardView
{
    [SerializeField] private TextMeshProUGUI CoinAmount;
    [SerializeField] private Image Coin;
    // Start is called before the first frame update
    void Start()
    {
        SetRewardPrice();
        SetRewardText();
        foreach (var reward in Reward.Rewards)
        {
            if (reward.GetRewardType() == RewardType.Coins)
            {
                SetCoinAmount(reward.GetAmount().ToString());
                SetCoinSprite(reward.GetIcon());
            }
            // SetRewardSprite(reward.GetIcon());
            // SetRewardAmount(reward.GetAmount().ToString());
        }
    }

    private void SetCoinSprite(Sprite icon)
    {
        Coin.sprite = icon;
    }

    private void SetCoinAmount(string amount)
    {
        CoinAmount.text = amount;
    }
}
