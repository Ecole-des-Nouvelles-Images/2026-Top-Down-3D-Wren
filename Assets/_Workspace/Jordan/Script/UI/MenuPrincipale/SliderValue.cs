using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class SliderValue : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

        void Start()
        {
            UpdateValue(_slider.value);
            _slider.onValueChanged.AddListener(delegate { UpdateValue(_slider.value); });
        }

        void UpdateValue(float value)
        {
            _valueText.text = Mathf.RoundToInt(value*100).ToString();
        }
    }
}