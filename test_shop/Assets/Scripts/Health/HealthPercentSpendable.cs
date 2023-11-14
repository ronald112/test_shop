using System;
using Core;
using UnityEngine;

namespace Health
{
    [Serializable]
    public class HealthPercentSpendable : ISpendable
    {
        [SerializeField] private float amountToSpendPercent;
        [SerializeField] private string name;
        private bool isCanSpend;

        public bool IsCanSpend()
        {
            if (HealthManager.Instance.HealthAmount > 0)
                return true;
            return false;
        }

        public event Action<bool> OnCanSpendChanged;

        public bool TrySpend()
        {
            HealthManager.Instance.HealthAmount -= HealthManager.Instance.HealthAmount * amountToSpendPercent;
            return true;
        }

        public string GetName()
        {
            return name;
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