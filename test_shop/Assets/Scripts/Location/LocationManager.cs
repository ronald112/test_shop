using System;

namespace Location
{
    public class LocationManager
    {
        private static LocationManager locationManager = new LocationManager();
        public static LocationManager Instance
        {
            get => locationManager;
            private set => locationManager = value;
        }

        public static readonly LocationType DefaultLocationName = LocationType.Undefined;

        private event Action<LocationType> innerOnLocationChanged;
        public event Action<LocationType> onLocationChanged
        {
            add
            {
                innerOnLocationChanged += value;
                value(locationName);
            }
            remove => innerOnLocationChanged -= value;
        }

        // todo: init from somewhere
        private LocationType locationName = DefaultLocationName;
        public LocationType LocationName
        {
            get => locationName;
            set
            {
                locationName = value;
                innerOnLocationChanged?.Invoke(locationName);
            } 
        }
    }
}
