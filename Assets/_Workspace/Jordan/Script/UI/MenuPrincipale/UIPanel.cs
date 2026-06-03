using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class UIPanel : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Selectable _buttonSelectable;
        
        public void Awake() {
            if(_buttonSelectable!=null) _buttonSelectable.Select();
        }
    }
}
