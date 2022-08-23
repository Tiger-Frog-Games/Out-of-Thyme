using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelInt OnScoreChange;
        [SerializeField] private EventChannelFloat OnMultiplierChange;

        [SerializeField] private EventChannel OnGameOver;
        [SerializeField] private EventChannelInt OnGameOverScoreBroadcast;
        
        [SerializeField] private EventChannelRequests OnRequestCompletedSuccess;
        [SerializeField] private EventChannelRequests OnRequestCompletedFailure;
        [SerializeField] private EventChannelItemData OnFailedItemSold;
        
        private int _currentScore;
        private float _currentMultiplier = 1;

        [SerializeField] private float multiplierChangeOnSuccess = .1f;
        [SerializeField] private float multiplierChangeOnFailure = -.1f;
        [SerializeField] private float multiplierChangeOnWrongItem = -.2f;
        [SerializeField] private float lowestMultiplier = .5f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnRequestCompletedSuccess.OnEvent += OnRequestCompletedSuccessOnOnEvent;
            OnRequestCompletedFailure.OnEvent += OnRequestCompletedFailureOnOnEvent;
            OnGameOver.OnEvent += OnGameOverOnOnEvent;
            
            OnFailedItemSold.OnEvent += OnFailedItemSoldOnOnEvent; 
        }
        
        private void OnDestroy()
        {
            OnRequestCompletedSuccess.OnEvent -= OnRequestCompletedSuccessOnOnEvent;
            OnRequestCompletedFailure.OnEvent -= OnRequestCompletedFailureOnOnEvent;
            OnGameOver.OnEvent -= OnGameOverOnOnEvent;
            
            OnFailedItemSold.OnEvent -= OnFailedItemSoldOnOnEvent; 
        }

        #endregion

        #region Methods

        private void OnGameOverOnOnEvent()
        {
            OnGameOverScoreBroadcast.RaiseEvent(_currentScore);
        }
        
        private void OnRequestCompletedSuccessOnOnEvent(Request obj)
        {
            changeMultiplier(multiplierChangeOnSuccess);
            changeScore(obj.RequestData.RequiredItem.ScoreValue);
        }
        
        private void OnRequestCompletedFailureOnOnEvent(Request obj)
        {
            changeMultiplier(multiplierChangeOnFailure);
        }

        private void OnFailedItemSoldOnOnEvent(ItemData obj)
        {
            changeMultiplier(multiplierChangeOnWrongItem);
        }

        private void changeScore(int scorechange)
        {
            _currentScore = this._currentScore + (int)(scorechange * _currentMultiplier);
            OnScoreChange.RaiseEvent(_currentScore);
        }
        
        private void changeMultiplier(float i)
        {
            _currentMultiplier += i;
            if (_currentMultiplier < lowestMultiplier) _currentMultiplier = lowestMultiplier;
            
            //round to .1 decimal place
            _currentMultiplier = Mathf.Round(_currentMultiplier * 10.0f) * 0.1f;
            
            OnMultiplierChange.RaiseEvent(_currentMultiplier);
        }
        
        
        #endregion
    }
}