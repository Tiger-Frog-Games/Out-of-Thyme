using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class ReciepeCombinerAnimator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private InteractableRecipeCombiner _recipeCombiner;
        [SerializeField] private GameObject thingToRotate;
        [SerializeField] private Vector3 rotateSpeed ;
        #endregion

        #region Unity Methods

        private void Update()
        {
            if(!_recipeCombiner._isTransoforming) return;

            thingToRotate.transform.Rotate(rotateSpeed * Time.deltaTime);
        }

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
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