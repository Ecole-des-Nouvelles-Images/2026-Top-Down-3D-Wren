using _Workspace.Jordan.Script.Joueur;
using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Managers
{
    public class UIPlayerPanel : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _dash;
        private PlayerController _player;

        public void SetupUI(PlayerController player) 
        {
            player.OnHealthChangeNormalized += PlayerOnOnHealthChangeNormalized;
            player.OnDashReadyNormalized += PlayerOnOnDashReadyNormalized;
            _player = player;
        }

        private void PlayerOnOnDashReadyNormalized(object sender, float e)
        {
            _dash.fillAmount = e;
        }

        private void PlayerOnOnHealthChangeNormalized(object sender, float e)
        {
            _healthBar.fillAmount = e;
        }

        private void OnDestroy()
        {
            _player.OnHealthChangeNormalized -= PlayerOnOnHealthChangeNormalized;
            _player.OnDashReadyNormalized -= PlayerOnOnDashReadyNormalized;
        }
    }
}
