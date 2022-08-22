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

        [SerializeField] private int maxNumberOfItems;
        [SerializeField] private GameObject[] LocationOfHeldGameObjects;
        
        [SerializeField] private ItemTransformationType transformationType;
        
        [SerializeField] private RecipeList recipies;

         private List<Item> _heldItemsToBeCombined = new ();
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

        private void OnValidate()
        {
            if (maxNumberOfItems != LocationOfHeldGameObjects.Length)
            {
                Debug.LogWarning($"Warning {this.gameObject} - Max Numbers of Items and length of Location of Held Game Object must be equal");
            }
        }

        #endregion

        #region Methods

        private void completeRecipe()
        {
            if(_heldItemsToBeCombined.Count == 0) return;

            _heldItem = _heldItemsToBeCombined[0];

            for (int i = 1; i < _heldItemsToBeCombined.Count; i++)
            {
                ObjectPool.Store(_heldItemsToBeCombined[i].gameObject);
            }
            
            _heldItem.SetUpItem(_currentRecipe.CreatedItem);
            _heldItem.transform.position = heldObjectRoot.transform.position;
            
            _heldItemsToBeCombined.Clear();
            _currentRecipe = default;
            _isTransoforming = false;
        }
        
        
        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            if(_heldItemsToBeCombined.Count  >= maxNumberOfItems) return;
            
            _heldItemsToBeCombined.Add(playerItemHolder.HeldItem);

            
            
            playerItemHolder.PlaceItem(LocationOfHeldGameObjects[_heldItemsToBeCombined.Count-1].transform);
            
            startTransforming();
        }

        protected override void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            //Pick up completed Item
            if (_heldItem != null)
            {
                base.pickUpFromItemHolder(playerItemHolder);
                if(_isTransoforming == true) stopTransforming();
                return;
            }
            
            //Cant interupt a recipe being transformed
            if(_isTransoforming) return;
            
            //Pick up the last one in
            if (_heldItemsToBeCombined.Count > 0)
            {
                var tempItem = _heldItemsToBeCombined[_heldItemsToBeCombined.Count - 1];
                _heldItemsToBeCombined.RemoveAt(_heldItemsToBeCombined.Count - 1);
                
                playerItemHolder.PickUpItem(tempItem);
                
            }
            
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void startTransforming()
        {
            if(_isTransoforming) stopTransforming();
            
            _currentRecipe = recipies.GetValidRecipie(transformationType, _heldItemsToBeCombined);
            
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