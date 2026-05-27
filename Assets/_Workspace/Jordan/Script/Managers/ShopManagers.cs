using _Workspace.Jordan.Script.Joueur;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private Canvas _canvas;

        public void BuyDamageUpgrade()
        {
            _playerAttack.UpgradeDamage(5f);
        }

        public void BuyAttackSpeedUpgrade()
        {
            _playerController.UpgradeAttackSpeed(0.1f);
        }

        public void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                //ouvre les uis
                // se deplace dedans
            }
        }
    }
}
