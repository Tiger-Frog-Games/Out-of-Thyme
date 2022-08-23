using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace TigerFrogGames
{
    public class UIManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private InputActionAsset actions;
        private InputAction openMenuButton;


        [SerializeField] private GameObject PauseMenuRoot;
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject OptionsMenu;

        [SerializeField] private GameObject ScreenGrayer;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            openMenuButton = actions.FindAction("Open Menu");
            if (openMenuButton != null)
            {
                openMenuButton.performed += OnOpenMenuButtonPress;
            }
            
            
            //First Time Clean up
            PauseMenuRoot.SetActive(false);
            
        }

        private void Start()
        {
            openMenuButton.Enable();
        }

        private void OnDestroy()
        {
            if (openMenuButton != null)
            {
                openMenuButton.performed -= OnOpenMenuButtonPress;
            }
        }

        
        #endregion

        #region Methods

        private void OnOpenMenuButtonPress(InputAction.CallbackContext obj)
        {
            if (GameStateManager.Instance.CurrentGameState == GameState.Gameplay )
            {
                pauseGame();
            }
            else
            {
                unPauseGame();
            }
        }

        private void pauseGame()
        {
            GameStateManager.Instance.SetState(GameState.Paused);
            cleanUpPauseMenu();
            PauseMenuRoot.SetActive(true);
            ScreenGrayer.SetActive(true);
        }

        private void unPauseGame()
        {
            GameStateManager.Instance.SetState(GameState.Gameplay);
            PauseMenuRoot.SetActive(false);
            ScreenGrayer.SetActive(false);
        }

        /// <summary>
        /// Makes it so the main Pause Menu is enabled and other menus are hidden.
        /// </summary>
        private void cleanUpPauseMenu()
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }


        public void ShowOptions()
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }
        
        public void HideOptions()
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
        }

        public void QuitToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
        
        #endregion
    }
}