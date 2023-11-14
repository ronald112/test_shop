using System;
using Core;
using Unity.Plastic.Newtonsoft.Json.Schema;
using UnityEngine;
using UnityEngine.Serialization;

namespace Location
{
    [Serializable]
    public class LocationSpendable : ISpendable
    {
        [SerializeField] private LocationType locationName;
        private bool isCanSpend;
        private event Action<bool> innerOnCanSpendChanged;
        public event Action<bool> OnCanSpendChanged
        {
            add
            {
                innerOnCanSpendChanged += value;
                value(IsCanSpend());
            }
            remove => innerOnCanSpendChanged -= value;
        }
        
        public bool IsCanSpend()
        {
            return (int)LocationManager.Instance.LocationName == (int)locationName;
        }
        
        public bool TrySpend()
        {
            if (!IsCanSpend())
                return false;
            return true;
        }
        
        public void InitAction()
        {
            LocationManager.Instance.onLocationChanged += _ => InvokeIfSpendChanged();
        }

        private void InvokeIfSpendChanged()
        {
            if (IsCanSpend() != isCanSpend)
            {
                isCanSpend = !isCanSpend;
                innerOnCanSpendChanged?.Invoke(isCanSpend);
            }
        }
    }
}