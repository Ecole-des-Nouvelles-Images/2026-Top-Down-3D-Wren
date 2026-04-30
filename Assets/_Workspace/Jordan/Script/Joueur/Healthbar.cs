using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Workspace.Jordan.Script.Joueur
{
    public class Healthbar : MonoBehaviour
    { 
        public Slider Slider;

        public void SetMaxHealth(int health)
        {
            Slider.maxValue = health;
            Slider.value = health;
        }

        public void SetHealth(int health)
        {
            Slider.value = health;
        }
    }
}
