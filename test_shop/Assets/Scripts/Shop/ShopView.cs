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
            foreach (var bundle in shopSettingsSo.Bundles)
            {
                if (bundle.Spendables == null || bundle.Rewards == null)
                {
                    Debug.LogWarning($"Bundle {bundle.BundleName} set up error");
                    continue;
                }
                
                var element = UnityEngine.Object.Instantiate(shopElementPrefab, panel.transform);

                InitBundleSpendables(bundle.Spendables, element);
                element.Setup(bundle.BundleName, () => OnSpendClick(bundle));
            }
        }

        private void OnSpendClick(ShopBundle bundleSpendables)
        {
            if (bundleSpendables.Spendables.Length <= 0)
                return;
            if (bundleSpendables.Spendables[0].SpendPipeline())
            {
                foreach (var bundleSpendablesReward in bundleSpendables.Rewards)
                {
                    bundleSpendablesReward.GiveReward();
                }
            }
        }

        private void InitBundleSpendables(ISpendable[] bundleSpendables, ShopElement element)
        {
            ISpendable spendableFirst = null;
            ISpendable spendableNode = null;
            foreach (var spendable in bundleSpendables)
            {
                if (spendableNode == null)
                {
                    spendable.InitAction();
                    spendableFirst = spendableNode = spendable;
                    spendable.OnCanSpendChanged += element.SetButtonEnable;
                    continue;
                }

                spendableNode.Next = spendable;
                spendableNode = spendable;
            }

            if (spendableFirst != null)
                element.SetButtonEnable(spendableFirst.IsCanSpendPipeline());
        }
    }
}
