using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Shop
{
    [CreateAssetMenu(fileName = nameof(ShopSettingsSO), menuName = "ScriptableObjects/" + nameof(ShopSettingsSO),
        order = 0)]
    public class ShopSettingsSO : ScriptableObject
    {
        public List<ShopBundle> Bundles = new();
    }
}