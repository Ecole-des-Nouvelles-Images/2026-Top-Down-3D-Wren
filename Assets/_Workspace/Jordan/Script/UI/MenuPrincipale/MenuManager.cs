using UnityEngine;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]  private GameObject _menuControle;
        [SerializeField]  private GameObject _menuOptions;
        [SerializeField]  private GameObject _menuCredit;

        public void OpenControles()
        {
            _menuControle.SetActive(true);
        }
        
        public void OpenOptions()
        {
            _menuOptions.SetActive(true);
        }
        
        public void OpenCredits()
        {
            _menuCredit.SetActive(true);
        }

        public void QuitControles()
        {
            _menuControle.SetActive(false);
        }
        
        public void QuitOptions()
        {
            _menuOptions.SetActive(false);
        }
        
        public void QuitCredits()
        {
            _menuCredit.SetActive(false);
        }
    }
}
