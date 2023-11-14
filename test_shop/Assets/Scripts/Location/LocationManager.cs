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
        public static readonly string DefaultLocationName = "Undefined";

        private event Action<string> innerOnLocationChanged;
        public event Action<string> onLocationChanged
        {
            add
            {
                innerOnLocationChanged += value;
                value(locationName);
            }
            remove => innerOnLocationChanged -= value;
        }

        // todo: init from somewhere
        private string locationName = DefaultLocationName;
        public string LocationName
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
