using System;
using Core;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthPercentReward : IReward
    {
        [SerializeField] public float healthAmount;
        
        public void GiveReward()
        {
            HealthManager.Instance.HealthAmount += HealthManager.Instance.HealthAmount * healthAmount;
        }
    }
}