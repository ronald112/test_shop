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
        private bool isCanSpend;
        private event Action<bool> innerOnCanSpendChanged;
        public event Action<bool> OnCanSpendChanged
        {
            add
            {
                innerOnCanSpendChanged += value;
                value(IsCanSpend());
            }
            remove => innerOnCanSpendChanged -= value;
        }

        public bool IsCanSpend()
        {
            if (amountToSpent > HealthManager.Instance.HealthAmount)
                return false;
            return true;
        }


        public bool TrySpend()
        {
            if (!IsCanSpend())
                return false;
            HealthManager.Instance.HealthAmount -= amountToSpent;
            return true;
        }
        
        public void InitAction()
        {
            HealthManager.Instance.onHealthAmountChanged += _ => InvokeIfSpendChanged();
        }

        private void InvokeIfSpendChanged()
        {
            if (IsCanSpend() != isCanSpend)
            {
                isCanSpend = !isCanSpend;
                innerOnCanSpendChanged?.Invoke(isCanSpend);
            }
        }
    }
}