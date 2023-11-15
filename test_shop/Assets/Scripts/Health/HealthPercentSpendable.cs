using System;
using Core;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthPercentSpendable : ISpendable
    {
        [SerializeField] private float amountToSpendPercent;
        
        private event Action<bool> innerOnCanSpendChanged;
        public event Action<bool> OnCanSpendChanged
        {
            add => innerOnCanSpendChanged += value;
            remove => innerOnCanSpendChanged -= value;
        }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            HealthManager.Instance.onHealthAmountChanged += _ => innerOnCanSpendChanged?.Invoke(IsCanSpendPipeline());
        }
        
        public bool IsCanSpendPipeline()
        {
            if (HealthManager.Instance.HealthAmountTemp == null)
                HealthManager.Instance.HealthAmountTemp = HealthManager.Instance.HealthAmount;
                
            if (HealthManager.Instance.HealthAmountTemp > 0)
            {
                HealthManager.Instance.HealthAmountTemp -= HealthManager.Instance.HealthAmountTemp * amountToSpendPercent;
                Next?.IsCanSpendPipeline();
                HealthManager.Instance.HealthAmountTemp = null;
            }
            else
            {
                HealthManager.Instance.HealthAmountTemp = null;
                return false;
            }
            return true;
        }

        public bool SpendPipeline()
        {
            if (HealthManager.Instance.HealthAmountTemp == null)
                HealthManager.Instance.HealthAmountTemp = HealthManager.Instance.HealthAmount;
                
            if (HealthManager.Instance.HealthAmountTemp > 0)
            {
                HealthManager.Instance.HealthAmountTemp -= HealthManager.Instance.HealthAmountTemp * amountToSpendPercent;
                Next?.SpendPipeline();
                if (HealthManager.Instance.HealthAmountTemp != null)
                {
                    HealthManager.Instance.HealthAmount = HealthManager.Instance.HealthAmountTemp.Value;
                    HealthManager.Instance.HealthAmountTemp = null;
                }
            }
            else
            {
                HealthManager.Instance.HealthAmountTemp = null;
                return false;
            }
            return true;
        }
    }
}