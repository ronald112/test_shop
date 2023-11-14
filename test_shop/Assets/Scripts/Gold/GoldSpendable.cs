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
        [SerializeField] private string bundleName;

        private bool isCanSpend;
        public event Action<bool> OnCanSpendChanged;

        private bool IsCanSpend()
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
            CheckGoldAmountAndInvoke();
            return true;
        }

        private void CheckGoldAmountAndInvoke()
        {
            if (IsCanSpend() != isCanSpend)
            {
                isCanSpend = !isCanSpend;
                OnCanSpendChanged?.Invoke(isCanSpend);
            }
        }

        public string GetName()
        {
            return bundleName;
        }

        public void InitAction()
        {
            GoldManager.Instance.onGoldAmountChanged += _ => CheckGoldAmountAndInvoke();
        }
    }
}