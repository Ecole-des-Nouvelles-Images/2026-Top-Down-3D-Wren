using System;
using System.Collections.Generic;
using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.UI.InGame
{
    public class ReviveUI : MonoBehaviour
    {
        [SerializeField] private PlayerController _deadPlayer;
        [SerializeField] private string _targetTag;
        [SerializeField] private List<GameObject> _targets;
        [SerializeField] private ReviveZone _reviveZone;

        private void Start()
        {
            HideUI();
            
            _deadPlayer.OnHealthChange += DeadPlayerOnOnHealthChange;
        }

        private void OnDestroy()
        {
            _deadPlayer.OnHealthChange -= DeadPlayerOnOnHealthChange;
        }

        private void DeadPlayerOnOnHealthChange(object sender, float newHp)
        {
            print("les PV de mon joueur sont à " + newHp);
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

            if (_deadPlayer.IsDead)
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