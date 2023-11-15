using System;
using Core;
using UnityEngine;

namespace Location
{
    [Serializable]
    public class LocationSpendable : ISpendable
    {
        [SerializeField] private LocationType locationName;
        private event Action<bool> innerOnCanSpendChanged;

        public event Action<bool> OnCanSpendChanged
        {
            add => innerOnCanSpendChanged += value;
            remove => innerOnCanSpendChanged -= value;
        }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            LocationManager.Instance.onLocationChanged += _ => innerOnCanSpendChanged?.Invoke(IsCanSpendPipeline());
        }

        public bool IsCanSpendPipeline()
        {
            if (LocationManager.Instance.LocationNameTemp == null)
                LocationManager.Instance.LocationNameTemp = LocationManager.Instance.LocationName;
                
            if (LocationManager.Instance.LocationNameTemp == locationName)
            {
                LocationManager.Instance.LocationNameTemp = locationName;
                Next?.IsCanSpendPipeline();
                LocationManager.Instance.LocationNameTemp = null;
            }
            else
            {
                LocationManager.Instance.LocationNameTemp = null;
                return false;
            }
            return true;
        }

        public bool SpendPipeline()
        {
            return IsCanSpendPipeline();
        }
    }
}