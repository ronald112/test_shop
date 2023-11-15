using System;
using Core;
using UnityEngine;

namespace Location
{
    [Serializable]
    public class LocationSpendable : ISpendable
    {
        [SerializeField] private LocationType locationName;
        public event Action<bool> OnCanSpendChanged;

        public ISpendable Head { get; set; }

        public ISpendable Next { get; set; }
        
        public void InitAction()
        {
            LocationManager.Instance.onLocationChanged += _ => 
            {
                Head.ClearBufferPipeline();
                OnCanSpendChanged?.Invoke(Head.CalculatedBufferPipeline());
            };
        }

        public bool CalculatedBufferPipeline()
        {
            if (LocationManager.Instance.LocationNameBuffer == null)
                LocationManager.Instance.LocationNameBuffer = LocationManager.Instance.LocationName;
                
            if (LocationManager.Instance.LocationNameBuffer == locationName)
            {
                LocationManager.Instance.LocationNameBuffer = locationName;
                if (Next == null)
                    return true;
                return Next.CalculatedBufferPipeline();
            }
            return false;
        }

        public bool ApplyBufferPipeline()
        {
            if (LocationManager.Instance.LocationNameBuffer != null)
            {
                LocationManager.Instance.LocationName = LocationManager.Instance.LocationNameBuffer.Value;
                LocationManager.Instance.LocationNameBuffer = null;
            }

            Next?.ApplyBufferPipeline();
                
            return false;
        }

        public void ClearBufferPipeline()
        {
            if (LocationManager.Instance.LocationNameBuffer != null)
                LocationManager.Instance.LocationNameBuffer = null;
            Next?.ClearBufferPipeline();
        }
    }
}