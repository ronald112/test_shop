using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Location
{
    public class LocationNameView : MonoBehaviour
    {
        [SerializeField] private Text Text;
        [SerializeField] private Button DefaultLocationButton;

        private void Awake()
        {
            LocationManager.Instance.onLocationChanged += SetHealth;
            DefaultLocationButton.onClick.AddListener(OnAddGoldCheat);
        }

        private void OnAddGoldCheat()
        {
            LocationManager.Instance.LocationName = LocationManager.DefaultLocationName;
        }

        private void SetHealth(string value)
        {
            Text.text = value;
        }
    }
}