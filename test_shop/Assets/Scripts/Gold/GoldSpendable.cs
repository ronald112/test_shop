using System;
using Core;
using UnityEngine;

namespace Gold
{
    [Serializable]
    public class GoldSpendable : ISpendable
    {
        [SerializeField] private int amountToSpent;

        private event Action<bool> innerOnCanSpendChanged;
        public event Action<bool> OnCanSpendChanged
        {
            add => innerOnCanSpendChanged += value;
            remove => innerOnCanSpendChanged -= value;
        }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            GoldManager.Instance.onGoldAmountChanged += _ => innerOnCanSpendChanged?.Invoke(IsCanSpendPipeline());
        }
        
        public bool IsCanSpendPipeline()
        {
            if (GoldManager.Instance.GoldAmountTemp == null)
                GoldManager.Instance.GoldAmountTemp = GoldManager.Instance.GoldAmount;
                
            if (GoldManager.Instance.GoldAmountTemp >= amountToSpent)
            {
                GoldManager.Instance.GoldAmountTemp -= amountToSpent;
                Next?.IsCanSpendPipeline();
                GoldManager.Instance.GoldAmountTemp = null;
            }
            else
            {
                GoldManager.Instance.GoldAmountTemp = null;
                return false;
            }
            return true;
        }

        public bool SpendPipeline()
        {
            if (GoldManager.Instance.GoldAmountTemp == null)
                GoldManager.Instance.GoldAmountTemp = GoldManager.Instance.GoldAmount;
                
            if (GoldManager.Instance.GoldAmountTemp >= amountToSpent)
            {
                GoldManager.Instance.GoldAmountTemp -= amountToSpent;
                Next?.SpendPipeline();
                if (GoldManager.Instance.GoldAmountTemp != null)
                {
                    GoldManager.Instance.GoldAmount = GoldManager.Instance.GoldAmountTemp.Value;
                    GoldManager.Instance.GoldAmountTemp = null;
                }
            }
            else
            {
                GoldManager.Instance.GoldAmountTemp = null;
                return false;
            }
            return true;
        }
    }
}