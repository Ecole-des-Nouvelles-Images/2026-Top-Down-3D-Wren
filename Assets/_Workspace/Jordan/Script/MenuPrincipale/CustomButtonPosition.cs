using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CustomButtonPosition : MonoBehaviour
{
    [SerializeField] private GameObject _firstselectedbutton;
    [SerializeField] private GameObject _returnselectedbutton;
    
    public void StartJumping()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstselectedbutton);
    }
    
    public void GoBack()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_returnselectedbutton);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }
}
