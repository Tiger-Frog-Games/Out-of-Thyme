using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TigerFrogGames
{
    public class UIRequest : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelRequests OnRequestTimedOut;
        [SerializeField] private TMP_Text Name;
        
        private Request _request;

        
        [SerializeField] GameObject sliderBar;
        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        private void Update()
        {
            if (_request.TimeLeft < 0)
            {
                OnRequestTimedOut.RaiseEvent(_request);
            }

            _request.changeTime(Time.deltaTime);

            updateTimerBar();
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void updateTimerBar()
        {
            float maxTimeLeft = _request.RequestData.getTimeLeftToComplete();
            if (maxTimeLeft == -1 || maxTimeLeft == 0)
            {
                sliderBar.transform.localScale = new Vector3(1,1,1);
            }
            else
            {
                sliderBar.transform.localScale = new Vector3(_request.TimeLeft/maxTimeLeft,1,1);
            }
            
        }
        
        public void setUpRequest(Request request)
        {
            Name.text = request.RequestData.RequiredItem.Name;
            _request = request;
        }
        
        #endregion
    }
}