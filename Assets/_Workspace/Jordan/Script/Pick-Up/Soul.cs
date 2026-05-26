using UnityEngine;

namespace _Workspace.Jordan.Script.Pick_Up
{
    public class Soul : MonoBehaviour
    {
        [SerializeField] private int _soul;

        public bool SpendSoul(int amount)
        {
            if (_soul >= amount)
            {
                _soul -= amount;
            }
            return true;
        }
        public void AddSouls(int amount)
        {
            _soul += amount;
        }
    }
}
