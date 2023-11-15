using System;

namespace Core
{
    public interface ISpendable
    {
        public event Action<bool> OnCanSpendChanged;
        public ISpendable Head { get; set; }
        public ISpendable Next { get; set; }

        public void InitAction();
        public bool CalculatedBufferPipeline();
        public bool ApplyBufferPipeline();
        public void ClearBufferPipeline();
    }
}
