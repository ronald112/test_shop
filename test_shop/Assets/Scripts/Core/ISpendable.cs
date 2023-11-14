using System;

namespace Core
{
    public interface ISpendable
    {
        public event Action<bool> OnCanSpendChanged;
        public bool TrySpend();
        public void InitAction();
        public bool IsCanSpend();
    }
}
