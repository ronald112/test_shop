using Core;
using UnityEngine;

namespace Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private ShopSettingsSO shopSettingsSo;
        [SerializeField] private ShopElement shopElementPrefab;
        [SerializeField] private GameObject panel;

        private void Awake()
        {
            foreach (var spendable in shopSettingsSo.Spendables)
            {
                var element = UnityEngine.Object.Instantiate(shopElementPrefab, panel.transform);
                
                spendable.InitAction();
                element.Setup(spendable.GetName(), () => spendable.TrySpend());
                spendable.OnCanSpendChanged += value => element.SetButtonEnable(value);
            }
        }
    }
}
