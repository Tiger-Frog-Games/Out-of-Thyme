using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerSpawner : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelGameStart OnGameStart;
        [SerializeField] private GameObject[] PlayerSpawnLocations;
        [SerializeField] private GameObject PlayerPrefab;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnGameStart.OnEvent += OnGameStartOnOnEvent;
        }
        
        private void OnDestroy()
        {
            OnGameStart.OnEvent -= OnGameStartOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnGameStartOnOnEvent(StartGameData obj)
        {
            for (int i = 0; i < obj.NumberOfPlayers; i++)
            {
                Instantiate(PlayerPrefab, PlayerSpawnLocations[i].transform);
            }
        }

        #endregion
    }
}