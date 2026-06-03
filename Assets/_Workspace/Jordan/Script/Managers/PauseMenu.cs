using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Workspace.Jordan.Script.UI.InGame
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("Pause Menu")]
        [SerializeField] private GameObject _firstselectedbutton;
        [SerializeField] private GameObject _menuPause;

        private bool _paused = false;
        

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_paused)
                    Resume();
                else
                    Pause();
            }
        }
        public void OnPause(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed)
                return;

            TogglePause();
        }

        private void TogglePause()
        {
            if (_paused)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            _menuPause.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Pause()
        {
            _menuPause.SetActive(true);
            Time.timeScale = 0f;
            _paused = true;
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu_Principale");
        }

        public void SettingsMenu()
        {
            SceneManager.LoadScene("SettingsMenu");
        }
    }
}


