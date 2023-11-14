using System;

namespace Core
{
    public interface IReward
    {
        public Action onRewardGiven { get; set; }
        public bool TryGiveReward();
        public string GetName();
    }
}
