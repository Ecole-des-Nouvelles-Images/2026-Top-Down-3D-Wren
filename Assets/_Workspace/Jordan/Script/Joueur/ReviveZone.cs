using UnityEngine;

namespace _Workspace.Jordan.Script.Joueur
{
    public class ReviveZone : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;

        public void RevivePlayer()
        {
            _player.Revive();
        }
    }
}