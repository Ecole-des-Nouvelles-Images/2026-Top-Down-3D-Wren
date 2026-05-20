using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class SceneChanger : MonoBehaviour
    {
        public string NextSceneName;
    
        public void LoadScene()
        {
            SceneManager.LoadScene(NextSceneName);
            Debug.Log("Scene Charged is " + NextSceneName);
        }
    }
}
