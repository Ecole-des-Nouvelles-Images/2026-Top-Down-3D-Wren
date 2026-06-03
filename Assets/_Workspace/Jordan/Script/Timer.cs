using TMPro;
using UnityEngine;

namespace _Workspace.Jordan.Script
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _timesRemaining;
        [SerializeField] private TextMeshProUGUI _valueText;

        void Update()
        {
            if (_timesRemaining > 0)
            {
                _timesRemaining -= Time.deltaTime;

                if (_timesRemaining <= 0)
                {
                    _timesRemaining = 0;
                    EndOfTimes();
                }
            }

            _valueText.text = Mathf.CeilToInt(_timesRemaining).ToString();
        }

        void EndOfTimes()
        {
            Debug.Log("Temps écoulé !");
        }
    }
}