using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace TigerFrogGames
{
    /// <summary>
    /// Animates the player according to variables of other components on the the gameobject.
    ///
    /// A nice to have I am leaving hooks for is animating the character instead of being a capsule. 
    /// </summary>
    public class PlayerAnimator : MonoBehaviour
    {
        #region Variables

        [SerializeField] private Rigidbody rb;
        [SerializeField] private GameObject visual;
        [SerializeField] private PlayerMovement playerMovement;
        
        [SerializeField] private float rotationDampen;
        #endregion

        #region Unity Methods

        private void Start()
        {
            //face to player towards the screen on start. - Should probally do something more interesting and have them placeable per level but this works for the demo
            visual.transform.rotation = new Quaternion(0, 1, 0, 0);
        }

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
            RotateVisual();
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        public void PlayUseAnimtion(String animationToPlay)
        {
            print($"Playing Animation - {animationToPlay}");
        }
        
        private void RotateVisual()
        {
            if (rb.velocity.magnitude > 0.1f)
            {
                Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);
                
                visual.transform.rotation = Quaternion.Slerp(visual.transform.rotation, desiredRotation, Time.deltaTime * rotationDampen );
            }
        }
        
        #endregion
    }
}