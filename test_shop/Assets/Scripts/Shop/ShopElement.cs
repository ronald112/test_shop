using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shop
{
    public class ShopElement : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Button _button;
        [SerializeField] private Image _panel;

        private string ConvertIntegerToString(int N)
        {
            string arr = "";
            while (N != 0)
            {
                arr += (char)(N % 10 + '0');
                N /= 10;
            }
 
            return arr;
        }
        
        public void Setup(string name, Action onClick)
        {
            _text.text = name;
            var hashCode = ConvertIntegerToString(name.GetHashCode()).Substring(0, 6);

            if (ColorUtility.TryParseHtmlString("#" + hashCode, out Color color))
                _panel.color = color;
            _button.onClick.AddListener(() => onClick());
        }

        public void SetButtonEnable(bool value)
        {
            _button.interactable = value;
        }
    }
}