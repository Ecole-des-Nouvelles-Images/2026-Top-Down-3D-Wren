using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class UIPanel : MonoBehaviour
    {
        [Header("Button Selected")]
        [SerializeField] private Selectable _buttonSelectable;
    
        public void OpenPanel() {
            gameObject.SetActive(true);
            if(_buttonSelectable!=null) _buttonSelectable.Select();
        }
    }
}
