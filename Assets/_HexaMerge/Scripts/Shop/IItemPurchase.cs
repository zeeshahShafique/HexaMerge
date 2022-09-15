namespace _HexaMerge.Scripts.Shop
{
    public interface IItemPurchase
    
    {
        void PurchaseSuccess(IAPTypeSO iAPItem);
        void PurchaseFail(IAPTypeSO iAPItem);
    }
}