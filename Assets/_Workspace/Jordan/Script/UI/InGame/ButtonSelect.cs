using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.UI.InGame
{
    public class ButtonSelect : MonoBehaviour
    {
        public GameObject Firstselectedbutton;
        private void OnEnable()
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(Firstselectedbutton);
        }
    }
}
