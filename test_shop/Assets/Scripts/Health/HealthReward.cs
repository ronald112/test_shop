using System;
using Core;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthReward : IReward
    {
        [SerializeField] public float healthAmount;
        
        public void GiveReward()
        {
            HealthManager.Instance.HealthAmount += healthAmount;
        }
    }
}