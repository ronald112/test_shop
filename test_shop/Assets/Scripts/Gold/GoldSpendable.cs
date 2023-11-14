using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gold
{
    [Serializable]
    public class GoldSpendable : ISpendable
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
            if (amountToSpent > GoldManager.Instance.GoldAmount)
                return false;
            return true;
        }

        public bool TrySpend()
        {
            if (!IsCanSpend())
                return false;
            GoldManager.Instance.GoldAmount -= amountToSpent;
            return true;
        }

        private void InvokeIfSpendChanged()
        {
            if (IsCanSpend() != isCanSpend)
            {
                isCanSpend = !isCanSpend;
                innerOnCanSpendChanged?.Invoke(isCanSpend);
            }
        }
        
        public void InitAction()
        {
            GoldManager.Instance.onGoldAmountChanged += _ => InvokeIfSpendChanged();
        }
    }
}