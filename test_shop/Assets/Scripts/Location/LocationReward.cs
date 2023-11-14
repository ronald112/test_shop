using System;
using Core;
using UnityEngine;

namespace Location
{
    [Serializable]
    public class LocationReward : IReward
    {
        [SerializeField] public LocationType newLocation;
        
        public void GiveReward()
        {
            LocationManager.Instance.LocationName = newLocation;
        }
    }
}