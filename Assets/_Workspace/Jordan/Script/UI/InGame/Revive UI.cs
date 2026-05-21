using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.UI.InGame
{
    public class ReviveUI : MonoBehaviour
    {
        [SerializeField] private PlayerController _deadPlayer;
        [SerializeField] private string _targetTag = "Player";
        [SerializeField] private List<GameObject> _targets;

        private void Start()
        {
            HideUI();
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Enter");
            if (!other.CompareTag(_targetTag)) return;

            PlayerController otherPlayer = other.GetComponentInParent<PlayerController>();

            if (otherPlayer == null)
            {
                Debug.LogWarning("pas de PlayerController !");
                return;
            }

            if (otherPlayer == _deadPlayer) return;

            if (_deadPlayer._isDead)
            {
                ShowUI();

                ReviveZone reviveZone = _deadPlayer.GetComponent<ReviveZone>();

                if (reviveZone != null)
                {
                    otherPlayer.SetReviveTarget(reviveZone);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("Exit");
            if (!other.CompareTag(_targetTag)) return;

            PlayerController otherPlayer = other.GetComponentInParent<PlayerController>();

            if (otherPlayer == null)
            {
                Debug.LogWarning("pas de PlayerController !");
                return;
            }

            if (otherPlayer == _deadPlayer) return;

            HideUI();

            otherPlayer.ClearReviveTarget();
        }

        private void ShowUI()
        {
            foreach (var target in _targets)
            {
                if (target != null)
                    target.SetActive(true);
            }
        }

        private void HideUI()
        {
            foreach (var target in _targets)
            {
                if (target != null)
                    target.SetActive(false);
            }
        }
    }
}