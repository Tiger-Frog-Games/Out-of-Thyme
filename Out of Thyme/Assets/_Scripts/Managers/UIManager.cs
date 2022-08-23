using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        [SerializeField] private GameObject GameplayRoot;
        [SerializeField] private GameObject PauseMenuRoot;
        [SerializeField] private GameObject MainMenu;
        [SerializeField] private GameObject OptionsMenu;
        [SerializeField] private GameObject GameOverMenu;
        [SerializeField] private TMP_Text endScoreText;
        [SerializeField] private TMP_Text endHighScoreText;
        
        [SerializeField] private GameObject ScreenGrayer;
        [SerializeField] private GameObject LoadingScreen;

        [SerializeField] private EventChannelGameStart OnGameStart;
        [SerializeField] private EventChannel OnGameOver;
        [SerializeField] private EventChannelInt OnScoreReport;
        #endregion

        //THis should be in score manager?
        private int _prevHighScore;
        
        #region Unity Methods

        private void Awake()
        {
            openMenuButton = actions.FindAction("Open Menu");
            if (openMenuButton != null)
            {
                openMenuButton.performed += OnOpenMenuButtonPress;
            }
            OnGameStart.OnEvent += OnGameStartOnOnEvent;
            OnGameOver.OnEvent += OnGameOverOnOnEvent;
            OnScoreReport.OnEvent += OnScoreReportOnOnEvent;
            
            //First Time Clean up
            PauseMenuRoot.SetActive(false);
            LoadingScreen.SetActive(true);
            ScreenGrayer.SetActive(false);
            GameOverMenu.SetActive(false);

            if (PlayerPrefs.HasKey("HighScore"))
            {
                _prevHighScore = PlayerPrefs.GetInt("HighScore");
                endHighScoreText.text =""+_prevHighScore;
            }
            else
            {
                PlayerPrefs.SetInt("HighScore",100);
                _prevHighScore = 100;
                endHighScoreText.text ="100";
            }
            
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
            OnGameStart.OnEvent -= OnGameStartOnOnEvent;
            OnGameOver.OnEvent -= OnGameOverOnOnEvent;
            OnScoreReport.OnEvent -= OnScoreReportOnOnEvent;
        }

        
        #endregion

        #region Methods

        private void OnGameStartOnOnEvent(StartGameData obj)
        {
            LoadingScreen.SetActive(false);
        }
        
        private void OnGameOverOnOnEvent()
        {
            GameOverMenu.SetActive(true);
            ScreenGrayer.SetActive(true);
            GameplayRoot.SetActive(false);
        }
        
        private void OnScoreReportOnOnEvent(int obj)
        {
            endScoreText.text = ""+obj;

            if (obj > _prevHighScore)
            {
                PlayerPrefs.SetInt("HighScore",obj);
            }
            
        }
        
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
            if(GameOverMenu.activeSelf) return;
            
            GameStateManager.Instance.SetState(GameState.Paused);
            cleanUpPauseMenu();
            PauseMenuRoot.SetActive(true);
            ScreenGrayer.SetActive(true);
            GameplayRoot.SetActive(false);
        }

        public void unPauseGame()
        {
            GameStateManager.Instance.SetState(GameState.Gameplay);
            PauseMenuRoot.SetActive(false);
            ScreenGrayer.SetActive(false);
            GameplayRoot.SetActive(true);
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
            MainMenu.SetActive(false);
            OptionsMenu.SetActive(true);
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