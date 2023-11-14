using System;
using Core;
using UnityEngine;

namespace Gold
{
    [Serializable]
    public class GoldReward : IReward
    {
        [SerializeField] public int goldAmount;
        
        public void GiveReward()
        {
            GoldManager.Instance.GoldAmount += goldAmount;
        }
    }
}