using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TigerFrogGames
{
    public class TimeManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelGameStart OnGameStart;
        [SerializeField] private EventChannel OnGameOver;
        
        [SerializeField] private EventChannelInt minChange;
        [SerializeField] private EventChannelInt secChange;

        [SerializeField] private EventChannel OnTyhmeAdded;
        
        [SerializeField] private float gameRate;
        /// <summary>
        /// Time is added in seconds
        /// </summary>
        [SerializeField] private int TimeThymeAdds;
        
        [Header("Current Time -- Debug only ")]

        [SerializeField] private int currentMin_InGame;
        [SerializeField] private int currentSec_InGame;
        
        private float currentTime_InGame;
        
        private float _maxTime;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            enabled = false;
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
            OnTyhmeAdded.OnEvent += OnThymeAddedEvent;
            OnGameStart.OnEvent += OnGameStartOnOnEvent;
        }
        
        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
            OnTyhmeAdded.OnEvent -= OnThymeAddedEvent;
            OnGameStart.OnEvent -= OnGameStartOnOnEvent;
        }

        private void Update()
        {
            //Convert current real time to game time
            currentTime_InGame -= Time.deltaTime * gameRate;

            
            currentMin_InGame = (int) currentTime_InGame / 3600;
            currentSec_InGame = (int) currentTime_InGame / 60;

            BroadCastTime();

            if ( currentTime_InGame <= 0 )
            {
                GameStateManager.Instance.SetState(GameState.Paused);
                OnGameOver.RaiseEvent();
            }

        }

        
        
        private void OnValidate()
        {
            if (gameRate <= 0)
            {
                Debug.LogError("Game rate can not be zero or negative");
            }
        }

        #endregion

        #region Methods

        private int lastMinuteBroadcasted;
        private int lastSecBroadcasted;
        private void BroadCastTime()
        {
            if (currentMin_InGame != lastMinuteBroadcasted)
            {
                minChange.RaiseEvent( currentMin_InGame );
                lastMinuteBroadcasted = currentMin_InGame;
            }

            if (currentSec_InGame != lastSecBroadcasted)
            {
                secChange.RaiseEvent(currentSec_InGame);
                lastSecBroadcasted = currentSec_InGame;
            }
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void OnGameStartOnOnEvent(StartGameData obj)
        {
            currentTime_InGame = CalculateTime(obj.StartMin,obj.StartSec);
            _maxTime = currentTime_InGame;
            
            enabled = true;
        }

        private void OnThymeAddedEvent()
        {
            currentTime_InGame = Mathf.Clamp(currentTime_InGame + (TimeThymeAdds * 60) , 0, _maxTime);
            BroadCastTime();
        }

        public static float CalculateTime(float min, float sec)
        {
            return (((min * 3600) + (sec * 60)));
        }
        
        #endregion
    }
}