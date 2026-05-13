using _Workspace.Jordan.Script.Joueur;
using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    public class PickUpBlood : MonoBehaviour
    {
        public Blood Blood;
    
        public void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //DoVFX();
                Destroy(gameObject);
                Blood.Apply(playerLife: collision.gameObject.GetComponent<PlayerLife>());
                Debug.Log("Pick Up Blood");
                Debug.Log(Blood._amount + "récupère de la vie");
            }
        }

        // private void DoVFX()
        // {
        //     if (destroy != null)
        //     {
        //         // ParticleSystem clone = Instantiate(
        //         //     destroy,
        //         //     transform.position,
        //         //     destroy.transform.rotation
        //         );
        //         clone.Play();
        //         Destroy(clone.gameObject, 3);
        //     } 
        // }
    }
}
