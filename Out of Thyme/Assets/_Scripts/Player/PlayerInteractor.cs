using System;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerInteractor : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject interactRayRoot;

        [SerializeField] private LayerMask detectedInteratablesLayer;
        
        [SerializeField] private PlayerInputGameplay playerInput;
        [SerializeField] private PlayerItemHolder playerItemHolder;
        private Interactable _hoveredInteractable;

        [SerializeField] private PlayerAnimator _playerAnimator;
        
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
            CheckForInteractable();
            
            if (playerInput.frameInput.Use)
            {
                _playerAnimator.PlayUseAnimtion(UseHeldItem());
            }

            if (playerInput.frameInput.PickUpOrPlace)
            {
                PickUpOrPlace();
            }
            
            void CheckForInteractable()
            {
                RaycastHit hit;
                _hoveredInteractable = null;
                //I would use a paid asset SensorToolKit 2 here to make it so that you can detect in a small cylinder area so it would have a dead band to detected items. But I did not want to use any assets in this demo
                if (Physics.Raycast(interactRayRoot.transform.position, -Vector3.up, out hit, detectedInteratablesLayer)) {
                    
                    if (hit.transform.TryGetComponent(out Interactable hitInteractable))
                    {
                        _hoveredInteractable = hitInteractable;
                    }
                }
            }

            string UseHeldItem()
            {
                return playerItemHolder.UseItem();
            }

            void PickUpOrPlace()
            {
                //Place
                if (playerItemHolder.HeldItem != null)
                {
                    playerItemHolder.DropItem();
                    return;
                }
                
                //Pickup
                if(_hoveredInteractable == null) return;
                
                _hoveredInteractable.Interact(playerItemHolder);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            
            
            if (_hoveredInteractable != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(interactRayRoot.transform.position,_hoveredInteractable.transform.position);
            }
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        public InteratableItemHolder GetInteratableItemHolderIsHovered()
        {
            if(_hoveredInteractable is InteratableItemHolder hoveredInteractableItemHolder)
            {
                return hoveredInteractableItemHolder;
            }
            return null;
        }
        
        #endregion
    }
}
