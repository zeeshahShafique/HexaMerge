using _HexaMerge.Scripts.UI.Cards;
using UnityEngine;
using UnityEngine.Purchasing;

public class PopulateShop : MonoBehaviour
{
    [SerializeField] private StoreSO Store;

    [SerializeField] private GameObject SmallCard;

    [SerializeField] private GameObject BigCard;

    [SerializeField] private GameObject NoAdsCard;

    private void Awake()
    {
        Initialize();
    }

    [ContextMenu("Initialize")]
    private void Initialize()
    {
        foreach (var rewardItem in Store.RewardItems)
        {
            if (rewardItem.Rewards.Count > 1)
            {
                var obj = Instantiate(BigCard, this.transform);
                obj.GetComponent<CardView>().SetReward(rewardItem);
            }
            else if (rewardItem.ProductType == ProductType.NonConsumable)
            {
                var obj = Instantiate(NoAdsCard, this.transform);
                obj.GetComponent<CardView>().SetReward(rewardItem);
            }
            else
            {
                var obj = Instantiate(SmallCard, this.transform);
                obj.GetComponent<CardView>().SetReward(rewardItem);
            }
        }
    }
}
