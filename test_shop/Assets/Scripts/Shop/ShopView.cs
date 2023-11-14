using System;
using System.Collections.Generic;
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
            foreach (var bundleSpendable in bundleSpendables.Spendables)
            {
                if (!bundleSpendable.TrySpend())
                    return;
            }

            foreach (var bundleSpendablesReward in bundleSpendables.Rewards)
            {
                bundleSpendablesReward.GiveReward();
            }
        }

        private void InitBundleSpendables(ISpendable[] bundleSpendables, ShopElement element)
        {
            foreach (var spendable in bundleSpendables)
            {
                spendable.InitAction();
                spendable.OnCanSpendChanged += value => OnCanSpendChanged(value, bundleSpendables, element);
            }
        }

        private void OnCanSpendChanged(bool value, ISpendable[] bundleSpendable, ShopElement element)
        {
            if (value)
            {
                foreach (var spendable in bundleSpendable)
                {
                    if (!spendable.IsCanSpend())
                    {
                        element.SetButtonEnable(false);
                        return;
                    }
                }
                element.SetButtonEnable(true);
                return;
            }
            element.SetButtonEnable(false);
        }
    }
}
