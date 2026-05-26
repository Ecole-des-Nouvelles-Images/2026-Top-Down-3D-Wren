using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    public class PickUpSoul : MonoBehaviour
    {
        public Soul Soul;
    
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //DoVFX();
                Destroy(gameObject);
                Debug.Log("Pick Up Soul");
            }
        }
    }
}
