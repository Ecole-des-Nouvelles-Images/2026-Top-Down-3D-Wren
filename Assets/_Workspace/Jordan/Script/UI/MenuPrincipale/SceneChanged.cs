using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class SceneChanged : MonoBehaviour
    {
        public void Start_level1()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameScene");
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Quit");
        }
        public void MainMenu()
        {
            SceneManager.LoadScene("MenuPrincipal");
        }
    }
}
