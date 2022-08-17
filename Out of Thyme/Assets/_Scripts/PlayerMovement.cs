using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerMovement : MonoBehaviour
    {
        #region Variables
        [SerializeField] private PlayerInputGameplay input;
        [SerializeField] private Rigidbody rb;
        
        [Header("Gameplay")]
        [SerializeField] private float playerSpeed;

        [SerializeField] private float dashMultiplier;
        [SerializeField] private float dashDecay;
        [SerializeField] private float dashCD;
        private float currentDashMultiplier = 1;
        public bool isDashing { private set; get; }
        
        
        private float _lastDashCD = -1;

        private bool _pausedState = true;
        private Vector3 _pausedVelocity;
        private Vector3 _pausedVelocityAngular;
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
            _lastDashCD -= Time.deltaTime;
            
            currentDashMultiplier = Math.Clamp(currentDashMultiplier-(dashDecay*Time.deltaTime), 1, dashMultiplier);
            
            if (_lastDashCD < 0 && input.frameInput.Dash == true)
            {
                _lastDashCD = dashCD;
                dash();
            }
        }

        private void FixedUpdate()
        {
            var x = input.frameInput.Move.x;
            var y = input.frameInput.Move.y;

            rb.velocity = new Vector3(x * playerSpeed *currentDashMultiplier,0, y * playerSpeed * currentDashMultiplier);
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            //re-enable after pausing
            if (newGameState == GameState.Gameplay && _pausedState == true)
            {
                rb.isKinematic = false;
                
                rb.velocity = _pausedVelocity;
                rb.angularVelocity = _pausedVelocityAngular;
                
                rb.WakeUp();
                
                this.enabled = true;
            }
            // pause
            if (newGameState == GameState.Paused)
            {
                _pausedState = this.enabled;

                if (_pausedState == true)
                {
                    _pausedVelocity = rb.velocity;
                    _pausedVelocityAngular = rb.angularVelocity;

                    rb.isKinematic = true;
                }
                this.enabled = false;  
            }
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void dash()
        {
            isDashing = true;
            currentDashMultiplier = dashMultiplier;
        }
        
        #endregion
    }
}