using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Workspace.Jordan.Script
{
    public class DeathScenes : MonoBehaviour
    {
        [SerializeField] private string _nameScene;
        [SerializeField]  private float _transitionDuration;
        
        private float _timer;
        private bool _isTransitioning = false;
        
        private void Update()
        {
            if (_isTransitioning)
            {
                _timer += Time.deltaTime;
                if (_timer >= _transitionDuration)
                {
                    SceneManager.LoadScene(_nameScene);
                }
            }
       
        }
    }
}
