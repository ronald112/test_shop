using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = nameof(ShopSettingsSO), menuName = "ScriptableObjects/" + nameof(ShopSettingsSO), order = 0)]
    public class ShopSettingsSO : ScriptableObject
    {
        [SerializeReference, SelectImplementation(typeof(ISpendable))]
        public List<ISpendable> Spendables = new List<ISpendable>();
        
        [SerializeReference, SelectImplementation(typeof(IReward))]
        public List<IReward> Rewards = new List<IReward>();
    }
}