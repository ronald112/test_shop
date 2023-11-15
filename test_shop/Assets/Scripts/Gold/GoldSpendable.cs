using System;
using Core;
using UnityEngine;

namespace Gold
{
    [Serializable]
    public class GoldSpendable : ISpendable
    {
        [SerializeField] private int amountToSpent;

        public event Action<bool> OnCanSpendChanged;

        public ISpendable Head { get; set; }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            GoldManager.Instance.onGoldAmountChanged += _ => 
            {
                Head.ClearBufferPipeline();
                OnCanSpendChanged?.Invoke(Head.CalculatedBufferPipeline());
            };
        }
        
        public bool CalculatedBufferPipeline()
        {
            if (GoldManager.Instance.GoldAmountBuffer == null)
                GoldManager.Instance.GoldAmountBuffer = GoldManager.Instance.GoldAmount;
                
            if (GoldManager.Instance.GoldAmountBuffer >= amountToSpent)
            {
                GoldManager.Instance.GoldAmountBuffer -= amountToSpent;
                if (Next == null)
                    return true;
                return Next.CalculatedBufferPipeline();
            }
            return false;
        }

        public bool ApplyBufferPipeline()
        {
            if (GoldManager.Instance.GoldAmountBuffer != null)
            {
                GoldManager.Instance.GoldAmount = GoldManager.Instance.GoldAmountBuffer.Value;
                GoldManager.Instance.GoldAmountBuffer = null;
            }

            Next?.ApplyBufferPipeline();
            return false;
        }

        public void ClearBufferPipeline()
        {
            if (GoldManager.Instance.GoldAmountBuffer != null)
                GoldManager.Instance.GoldAmountBuffer = null;
            Next?.ClearBufferPipeline();
        }
    }
}