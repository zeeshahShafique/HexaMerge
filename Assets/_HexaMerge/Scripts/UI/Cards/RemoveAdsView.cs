namespace _HexaMerge.Scripts.UI.Cards
{
    public class RemoveAdsView : SmallCardView
    {
        private void Start()
        {
            SetRewardPrice();
            SetRewardSprite();
            SetRewardText();
        }

        protected override void IAPCompleted(bool flag)
        {
            base.IAPCompleted(flag);
            if (flag == true)
                gameObject.SetActive(false);
        }

    }
}