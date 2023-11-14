using UnityEngine;
using UnityEngine.UI;

namespace Gold
{
    public class GoldAmountView : MonoBehaviour
    {
        [SerializeField] private Text Text;
        [SerializeField] private Button AddGoldButton;
        [SerializeField] private int addGoldCheatAmount = 100;

        private void Awake()
        {
            GoldManager.Instance.onGoldAmountChanged += SetGoldAmount;
            AddGoldButton.onClick.AddListener(OnAddGoldCheat);
        }

        private void OnAddGoldCheat()
        {
            GoldManager.Instance.GoldAmount += addGoldCheatAmount;
        }

        private void SetGoldAmount(int value)
        {
            Text.text = value.ToString();
        }
    }
}