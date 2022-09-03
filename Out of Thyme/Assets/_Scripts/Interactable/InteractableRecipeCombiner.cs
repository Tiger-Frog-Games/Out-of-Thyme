using System;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    /// <summary>
    /// If I have time I want to refactor this and Interactable Item Transformer to one class.
    /// </summary>
    public class InteractableRecipeCombiner : InteratableItemHolder
    {
        #region Variables
        
        [SerializeField] private ItemTransformationType transformationType;
        
        [SerializeField] private RecipeList recipies;
        
        private RecipeData _currentRecipe;
         
         public bool _isTransoforming { private set; get; }
         public float transformationPercent { private set; get; }
         private float timeToTransform;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
            _isTransoforming = false;  
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
                transformationPercent = 1 - (timeToTransform / _currentRecipe.TimeToTransform);
                if (timeToTransform < 0)
                {
                    completeRecipe();
                }
            }
        }
        
        #endregion

        #region Methods

        private void completeRecipe()
        {
            if(_heldItems.Count == 0) return;

            var _heldItem = _heldItems[0];

            for (int i = 1; i < _heldItems.Count; i++)
            {
                ObjectPool.Store(_heldItems[i].gameObject);
            }
            
            _heldItem.SetUpItem(_currentRecipe.CreatedItem);
            _heldItem.transform.position = heldObjectRoot.transform.position;
            
            _heldItems.Clear();
            
            _heldItems.Add(_heldItem);
            
            _currentRecipe = default;
            _isTransoforming = false;
        }
        
        
        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            base.addPlayerItemToItemHolder(playerItemHolder);
            
            startTransforming();
        }

        protected override void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            //Cant interupt a recipe being transformed
            if(_isTransoforming) return;
            
            base.pickUpFromItemHolder(playerItemHolder);
            
            
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void startTransforming()
        {
            if(_isTransoforming) stopTransforming();
            
            _currentRecipe = recipies.GetValidRecipie(transformationType, _heldItems);
            
            if(_currentRecipe.TimeToTransform == 0) return;

            
            timeToTransform = _currentRecipe.TimeToTransform ;
            _isTransoforming = true;
            
        }

        private void stopTransforming()
        {
            
            _isTransoforming = false;
            timeToTransform = -1;
        }
        
        
        
        #endregion
    }
}