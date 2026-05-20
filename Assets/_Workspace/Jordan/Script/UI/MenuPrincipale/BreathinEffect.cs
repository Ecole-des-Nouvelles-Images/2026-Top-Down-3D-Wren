using UnityEngine;

namespace _Workspace.Jordan.Script.MenuPrincipale
{
    public class BreathinEffect : MonoBehaviour
    {
        [Header("Breathing Settings")]
        public float speed = 1.5f;        // vitesse de la respiration
        public float intensity = 0.1f;    // amplitude de la respiration

        private Vector3 initialScale;

        void Start()
        {
            initialScale = transform.localScale;
        }

        void Update()
        {
            float scale = 1 + Mathf.Sin(Time.time * speed) * intensity;
            transform.localScale = initialScale * scale;
        }
    }
}
