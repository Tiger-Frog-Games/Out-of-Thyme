using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TigerFrogGames
{
    public class GrowingAnimator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private InteractableRecipeCombiner _recipeCombiner;
        [SerializeField] private GameObject BotLocation;
        [SerializeField] private GameObject TopLocation;
        [SerializeField] private GameObject thingToGrow;

        [SerializeField] private GameObject heldItemLocation;
        [SerializeField] private GameObject hiddenLocation;
        private Vector3 _heldItemStartLocation;
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
            _heldItemStartLocation = heldItemLocation.transform.position;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
        }

        private void Update()
        {
            if (!_recipeCombiner._isTransoforming)
            {
                thingToGrow.SetActive(false);
                heldItemLocation.transform.position = _heldItemStartLocation;
                return;
            }
            
            
            thingToGrow.SetActive(true);
            heldItemLocation.transform.position = hiddenLocation.transform.position;
            

            thingToGrow.transform.position =
                Vector3.Slerp(BotLocation.transform.position, TopLocation.transform.position, _recipeCombiner.transformationPercent);
            
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        #endregion
    }
}