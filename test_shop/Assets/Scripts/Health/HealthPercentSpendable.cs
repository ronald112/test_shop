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

        public ISpendable Head { get; set; }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            HealthManager.Instance.onHealthAmountChanged += _ => 
            {
                Head.ClearBufferPipeline();
                innerOnCanSpendChanged?.Invoke(Head.CalculatedBufferPipeline());
            };
        }
        
        public bool CalculatedBufferPipeline()
        {
            if (HealthManager.Instance.HealthAmountBuffer == null)
                HealthManager.Instance.HealthAmountBuffer = HealthManager.Instance.HealthAmount;
                
            if (HealthManager.Instance.HealthAmountBuffer > 0)
            {
                HealthManager.Instance.HealthAmountBuffer -= HealthManager.Instance.HealthAmountBuffer * amountToSpendPercent;
                if (Next == null)
                    return true;
                return Next.CalculatedBufferPipeline();
            }
            return false;
        }
        
        public bool ApplyBufferPipeline()
        {
            if (HealthManager.Instance.HealthAmountBuffer != null)
            {
                HealthManager.Instance.HealthAmount = HealthManager.Instance.HealthAmountBuffer.Value;
                HealthManager.Instance.HealthAmountBuffer = null;
            }

            Next?.ApplyBufferPipeline();
            return false;
        }

        public void ClearBufferPipeline()
        {
            if (HealthManager.Instance.HealthAmountBuffer != null)
                HealthManager.Instance.HealthAmountBuffer = null;
            Next?.ClearBufferPipeline();
        }
    }
}