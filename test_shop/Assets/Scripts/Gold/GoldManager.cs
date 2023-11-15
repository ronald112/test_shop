using System;

namespace Gold
{
    public class GoldManager
    {
        private GoldManager()
        {
        }
        
        private static GoldManager goldManager = new GoldManager();
        public static GoldManager Instance
        {
            get => goldManager;
            private set => goldManager = value;
        }

        public event Action<int> onGoldAmountChanged;

        public int? GoldAmountBuffer { get; set; } = null;

        // todo: init from somewhere
        private int goldAmount = 100;
        public int GoldAmount
        {
            get => goldAmount;
            set
            {
                goldAmount = value;
                onGoldAmountChanged?.Invoke(goldAmount);
            } 
        }
    }
}
