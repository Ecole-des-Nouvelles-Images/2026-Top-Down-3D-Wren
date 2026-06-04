using _Workspace.Jordan.Script.MenuPrincipale;
using UnityEngine;

public class DefeatManagers : MonoBehaviour
{
    [SerializeField] private SceneChanger _sceneChanger;

    private int _playersAlive;
    private bool _gameEnded = false;

    public void RegisterPlayer()
    {
        _playersAlive++;
    }

    public void PlayerDied()
    {
        if (_gameEnded) return;

        _playersAlive--;

        if (_playersAlive <= 0)
        {
            _playersAlive = 0;
            TriggerDefeat();
        }
    }

    private void TriggerDefeat()
    {
        _gameEnded = true;
        
        _sceneChanger.LoadScene();

        Debug.Log("Défaite : tous les joueurs sont morts");
    }
}
