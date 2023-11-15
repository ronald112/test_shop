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

        public event Action<LocationType> onLocationChanged;

        // todo: init from somewhere
        private LocationType locationName = DefaultLocationName;
        public LocationType LocationName
        {
            get => locationName;
            set
            {
                locationName = value;
                onLocationChanged?.Invoke(locationName);
            } 
        }

        public LocationType? LocationNameBuffer { get; set; } = null;
    }
}
