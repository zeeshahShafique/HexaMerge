using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/IAPs/IAPType", order = 1)]
public class IAPTypeSO : ScriptableObject
{
    public String SKU;
    public String Title;
    public String Price;
    // public List<Reward> Rewards;

}
