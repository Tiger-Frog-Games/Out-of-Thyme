using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TigerFrogGames
{
    public class MainMenu : MonoBehaviour
    {
        #region Variables
        
        [SerializeField] private GameObject MainMenuRoot;
        [SerializeField] private GameObject SoundSettings;
        [SerializeField] private GameObject HowToPlayScreen;
        
        [SerializeField] private GameObject LoadingScreen;
        
        [SerializeField] private Button addPlayerButton;
        [SerializeField] private Button removePlayerButton;

        [SerializeField] private TMP_Text numText;
        
        private int _numOfPlayers = 1;
        
        #endregion

        #region Unity Methods

        
        
        #endregion

        #region Methods

        public void startGame()
        {
            MainMenuRoot.SetActive(false);
            LoadingScreen.SetActive(true);

            GameStarter.Instance.loadGameScenes();
        }

        public void quitGame()
        {
            Application.Quit();
        }

        public void addPlayer()
        {
            _numOfPlayers = Math.Clamp(_numOfPlayers + 1, 1, 4);
            numText.text = "" + _numOfPlayers;
            GameStarter.Instance.changeNumberOfPlayer(_numOfPlayers);
            
            addPlayerButton.interactable = (_numOfPlayers != 4);
            removePlayerButton.interactable = (_numOfPlayers != 1);
        }

        public void removePlayer()
        {
            _numOfPlayers = Math.Clamp(_numOfPlayers - 1, 1, 4);
            numText.text = "" + _numOfPlayers;
            GameStarter.Instance.changeNumberOfPlayer(_numOfPlayers);
            
            addPlayerButton.interactable = (_numOfPlayers != 4);
            removePlayerButton.interactable = (_numOfPlayers != 1);
        }
        
        public void showSettings()
        {
            SoundSettings.SetActive(true);
            MainMenuRoot.SetActive(false);
        }

        public void hideSettings()
        {
            SoundSettings.SetActive(false);
            MainMenuRoot.SetActive(true);
        }
        
        public void ShowHowPlay()
        {
            MainMenuRoot.SetActive(false);
            HowToPlayScreen.SetActive(true);
        }
        
        public void HideHowToPlay()
        {
            MainMenuRoot.SetActive(true);
            HowToPlayScreen.SetActive(false);
        }

        #endregion
    }
}