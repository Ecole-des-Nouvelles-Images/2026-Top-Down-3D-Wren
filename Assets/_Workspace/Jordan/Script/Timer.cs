using TMPro;
using UnityEngine;
using _Workspace.Jordan.Script.MenuPrincipale;

namespace _Workspace.Jordan.Script
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _timesRemaining = 60f;
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private SceneChanger _sceneChanger;

        private bool _isFinished = false;

        void Update()
        {
            if (_isFinished) return;

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
            _isFinished = true;

            Debug.Log("Temps écoulé ! Victoire !");

            _sceneChanger.LoadScene();
        }
    }
}