using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public struct ShopBundle
    {
        public string BundleName;

        [SerializeReference, SelectImplementation(typeof(ISpendable))]
        public ISpendable[] Spendables;

        [SerializeReference, SelectImplementation(typeof(IReward))]
        public IReward[] Rewards;
    }
}