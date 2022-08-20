using System;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteractableItemTransformer : InteratableItemHolder
    {
        #region Variables

        //There should be a single place for this in a manager. 
        [SerializeField] private ItemTransformationType transformationType;

        public bool _isTransoforming { private set; get; }
        public float transformationPercent { private set; get; }
        private float timeToTransform;
        
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
            if (_isTransoforming)
            {
                timeToTransform -= Time.deltaTime;
                transformationPercent = 1 - (timeToTransform / _heldItem.ItemData.getTransformationTime(transformationType));
                if (timeToTransform < 0)
                {
                    transformHeldItem();
                }
            }
        }

        #endregion

        #region Methods

        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        [ContextMenu("Test")]
        private void transformHeldItem()
        {
            
            if(_heldItem == null ) return;

            _heldItem.SetUpItem(_heldItem.ItemData.getTransformedItemData(transformationType));
            
            startTransforming();
            
            _isTransoforming = false;
        }

        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            base.addPlayerItemToItemHolder(playerItemHolder);
            startTransforming();
        }

        protected override void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            base.pickUpFromItemHolder(playerItemHolder);
            if(_isTransoforming == true) stopTransforming();
        }

        private void startTransforming()
        {
            timeToTransform = _heldItem.ItemData.getTransformationTime(transformationType);

            if (timeToTransform != -1)
            {
                _isTransoforming = true;
            }
        }

        private void stopTransforming()
        {
            _isTransoforming = false;
            timeToTransform = -1;
        }

        #endregion
    }
}