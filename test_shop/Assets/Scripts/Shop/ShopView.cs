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
            bundleSpendables.Spendables[0].ClearBufferPipeline();
            if (bundleSpendables.Spendables[0].CalculatedBufferPipeline())
            {
                bundleSpendables.Spendables[0].ApplyBufferPipeline();
                foreach (var bundleSpendablesReward in bundleSpendables.Rewards)
                {
                    bundleSpendablesReward.GiveReward();
                }
            }
        }

        private void InitBundleSpendables(ISpendable[] bundleSpendables, ShopElement element)
        {
            ISpendable spendableHead = null;
            ISpendable spendableNext = null;
            foreach (var spendable in bundleSpendables)
            {
                spendable.InitAction();
                spendable.OnCanSpendChanged += element.SetButtonEnable;
                if (spendableNext == null)
                {
                    spendable.Head = spendableHead = spendableNext = spendable;
                    continue;
                }

                spendableNext.Next = spendable;
                spendableNext = spendable;
                spendable.Head = spendableHead;
            }

            if (spendableHead != null)
                element.SetButtonEnable(spendableHead.CalculatedBufferPipeline());
        }
    }
}
