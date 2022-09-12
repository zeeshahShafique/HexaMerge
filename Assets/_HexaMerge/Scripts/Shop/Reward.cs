using System;
using _HexaMerge.Scripts.Shop.Enum;
using UnityEngine;

namespace _HexaMerge.Scripts.Shop
{
    [Serializable]
    public class Reward 
    {
        [SerializeField] private int Amount;
        [SerializeField] private Sprite Icon;
        [SerializeField] private RewardType RewardType;

        public int GetAmount()
        {
            return Amount;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }

        public RewardType GetRewardType()
        {
            return RewardType;
        }
    
    }
}
