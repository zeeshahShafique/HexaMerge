using System;
using System.Collections.Generic;
using System.Linq;
using _HexaMerge.Scripts.Shop;
using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(menuName = "ScriptableObject/IAPs/Store", order = 1)]
public class StoreSO : ScriptableObject, IStoreListener
{
    public List<IAPTypeSO> RewardItems;
    [SerializeField] private FakeStoreUIMode FakeStoreUIMode;

    public bool IAPInProgress = false;
    
    [SerializeField] private DynamicOverlaySO StoreNotInitializedOverlay;

    
    
    [NonSerialized] private ConfigurationBuilder _configurationBuilder;
    [NonSerialized] private StandardPurchasingModule _purchasingModule;
    [NonSerialized] private IStoreController _storeController;
    [NonSerialized] private IExtensionProvider _extensionProvider;
    [NonSerialized] private IItemPurchase _purchaseListener;

    public bool IsInitialized
    {
        get
        {
            return _storeController != null && _extensionProvider != null;
        }
    }
    
    public void Initialize ()
    {
        Debug.Log("[INFO] Initializing IAP store");
        
        SetupStoreConfigurationBuilder();
        
        if (!IsInitialized)
        {
            UnityPurchasing.Initialize(this, _configurationBuilder);
        }
    }
    
    private void SetupStoreConfigurationBuilder ()
    {
        if (_configurationBuilder == null)
        {
            _purchasingModule = StandardPurchasingModule.Instance();
            _purchasingModule.useFakeStoreUIMode = FakeStoreUIMode;
 
            _configurationBuilder = ConfigurationBuilder.Instance(_purchasingModule);
            _configurationBuilder.AddProducts(RewardItems.Select(x => x.ProductDefinition));
        }
    }
    
    public IAPTypeSO GetIAPItem(string id)
    {
        return RewardItems.FirstOrDefault(x => x.SKU == id);
    }
    
    public Product GetProduct(string id)
    {
        var item = GetIAPItem(id);
        return item != null ? item.Product : null;
    }
    
    public bool PurchaseItemWithId(string id, IItemPurchase purchaseListener)
    {
        if (!IsInitialized) return false;
        if (_purchaseListener != null) return false;

        _purchaseListener = purchaseListener;
        _storeController.InitiatePurchase(id);

        IAPInProgress = true;
        return true;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"[ERROR][IAP] Purchasing failed to initialize. Reason: {error}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.LogError($"[INFO][IAP] Product purchased. Product: {purchaseEvent.purchasedProduct.definition.id}.");
        
        _purchaseListener?.PurchaseSuccess(GetIAPItem(purchaseEvent.purchasedProduct.definition.id));
        // var prod = GetIAPItem(purchaseEvent.purchasedProduct.definition.id);
        // if (prod.ProductType == ProductType.NonConsumable)
        //     RewardItems.Remove(prod);

        _purchaseListener = null;
        IAPInProgress = false;
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"[ERROR][IAP] Product purchase failed. Product: {product.definition.id}. Reason: {failureReason}");

        if (!IsInitialized)
        {
            StoreNotInitializedOverlay.EnableClickableOverlay("Weak Internet Connection");
            Initialize();
        }

        if (failureReason == PurchaseFailureReason.DuplicateTransaction)
        {
            var prod = GetIAPItem(product.definition.id);
            _purchaseListener?.PurchaseSuccess(prod);
        }
        else
        {
            _purchaseListener?.PurchaseFail(GetIAPItem(product.definition.id));
        }

        _purchaseListener = null;
    }
    
    private void AssignProductsToItems ()
    {
        for (int i = 0; i < RewardItems.Count; i++)
        {
            var item = RewardItems[i];
            item.Product = _storeController.products.WithID(item.SKU);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("[INFO][IAP] IAP Store initialized");
 
        _storeController = controller;
        _extensionProvider = extensions;
 
        AssignProductsToItems();
        
        RestorePurchases();
    }

    public bool RestorePurchases()
    {
        if (!IsInitialized) return false;
        _extensionProvider.GetExtension<IGooglePlayStoreExtensions>().RestoreTransactions(OnTransactionsRestored);
        return true;
    }
    
    void OnTransactionsRestored(bool success)
    {
        Debug.Log("[INFO][IAP] Restore Transactions completed.");
    }
}
