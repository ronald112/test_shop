using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Location
{
    [Serializable]
    public class LocationSpendable : ISpendable
    {
        [SerializeField] private string locationName;
        [SerializeField] private string bundleName;
        public event Action<bool> OnCanSpendChanged;


        public bool TrySpend()
        {
            LocationManager.Instance.LocationName = locationName;
            return true;
        }

        public string GetName()
        {
            return bundleName;
        }
        
        public void InitAction()
        {
            LocationManager.Instance.onLocationChanged += _ => TryLocationAmountAndInvoke();
        }

        private void TryLocationAmountAndInvoke()
        {
            OnCanSpendChanged?.Invoke(true);
        }
    }
}