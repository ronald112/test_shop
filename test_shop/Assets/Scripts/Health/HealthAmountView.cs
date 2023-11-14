using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Health
{
    public class HealthAmountView : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Button addGoldButton;
        [SerializeField] private int addHealthCheatAmount = 100;
        [SerializeField] private int healthThreshold = 10;
        [SerializeField] private Image panel;

        private void Awake()
        {
            HealthManager.Instance.onHealthAmountChanged += SetHealthAmount;
            addGoldButton.onClick.AddListener(OnAddGoldCheat);
        }

        private void OnAddGoldCheat()
        {
            HealthManager.Instance.HealthAmount += addHealthCheatAmount;
        }

        private void SetHealthAmount(float value)
        {
            text.text = value.ToString("F1");
            if (value < healthThreshold)
            {
                panel.color = Color.red;
            }
            else
            {
                panel.color = Color.green;
            }
        }
    }
}