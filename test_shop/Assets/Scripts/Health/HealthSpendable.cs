using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Health
{
    [Serializable]
    public class HealthSpendable : ISpendable
    {
        [SerializeField] private int amountToSpent;
        [SerializeField] private string bundleName;
        private bool isCanSpend;
        public event Action<bool> OnCanSpendChanged;

        public bool IsCanSpend()
        {
            if (amountToSpent > HealthManager.Instance.HealthAmount)
                return false;
            return true;
        }


        public bool TrySpend()
        {
            if (amountToSpent > HealthManager.Instance.HealthAmount)
                return false;
            HealthManager.Instance.HealthAmount -= amountToSpent;
            return true;
        }

        public string GetName()
        {
            return bundleName;
        }
        
        public void InitAction()
        {
            HealthManager.Instance.onHealthAmountChanged += _ => TryHealthAmountAndInvoke();
        }

        private void TryHealthAmountAndInvoke()
        {
            if (IsCanSpend() != isCanSpend)
            {
                isCanSpend = !isCanSpend;
                OnCanSpendChanged?.Invoke(isCanSpend);
            }
        }
    }
}