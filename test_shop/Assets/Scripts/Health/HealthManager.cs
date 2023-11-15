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

        public event Action<float> onHealthAmountChanged;
        
        // todo: init from somewhere
        private float healthAmount = 100;
        public float HealthAmount
        {
            get => healthAmount;
            set
            {
                healthAmount = value;
                onHealthAmountChanged?.Invoke(healthAmount);
            } 
        }

        public float? HealthAmountBuffer { get; set; } = null;
    }
}
