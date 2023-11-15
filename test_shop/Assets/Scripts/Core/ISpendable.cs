using System;

namespace Core
{
    public interface ISpendable
    {
        public event Action<bool> OnCanSpendChanged;
        public ISpendable Next { get; set; }

        public void InitAction();
        public bool SpendPipeline();
        public bool IsCanSpendPipeline();
    }
}
