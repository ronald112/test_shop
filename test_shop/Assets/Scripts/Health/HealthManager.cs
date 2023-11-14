using System;

namespace Health
{
    public class HealthManager
    {
        private static HealthManager healthManager = new HealthManager();
        public static HealthManager Instance
        {
            get => healthManager;
            private set => healthManager = value;
        }

        private event Action<float> innerOnHealthAmountChanged;
        public event Action<float> onHealthAmountChanged
        {
            add
            {
                innerOnHealthAmountChanged += value;
                value(healthAmount);
            }
            remove => innerOnHealthAmountChanged -= value;
        }
        
        // todo: init from somewhere
        private float healthAmount = 100;
        public float HealthAmount
        {
            get => healthAmount;
            set
            {
                healthAmount = value;
                innerOnHealthAmountChanged?.Invoke(healthAmount);
            } 
        }
    }
}
