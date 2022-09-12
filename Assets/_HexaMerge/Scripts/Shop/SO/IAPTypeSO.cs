using System;
using System.Collections.Generic;
using _HexaMerge.Scripts.Shop;
using UnityEngine;
using UnityEngine.Purchasing;

[CreateAssetMenu(menuName = "ScriptableObject/IAPs/IAPType", order = 1)]
public class IAPTypeSO : ScriptableObject
{
    public String SKU;
    public String Title;
    public float Price;
    public ProductType ProductType;
    public List<Reward> Rewards;

    public Product Product;
    
    ProductDefinition _productDefinition;
    public ProductDefinition ProductDefinition
    {
        get
        {
            if(_productDefinition == null)
            {
                _productDefinition = new ProductDefinition(SKU, ProductType);
            }
            return _productDefinition;
        }
    }
    
    

}
