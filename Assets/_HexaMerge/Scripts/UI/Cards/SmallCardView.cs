using _HexaMerge.Scripts.UI.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SmallCardView : StoreItemCardView
{
    [SerializeField] private TextMeshProUGUI RewardAmount;
    
    [SerializeField] protected Image RewardIcon;

    
    // Start is called before the first frame update
    void Start()
    {
        SetRewardPrice();
        SetRewardSprite();
        SetRewardText();
        SetRewardAmount();
    }
    
    private void SetRewardAmount()
    {
        RewardAmount.text = Reward.Rewards[0].GetAmount().ToString();
    }
    
    protected void SetRewardSprite()
    {
        RewardIcon.sprite = Reward.Rewards[0].GetIcon();
    }
}
