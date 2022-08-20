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
        [SerializeField] private GameObject itemPrefabToSpawn;
        
        [SerializeField] private RecipeList recipies;

         private List<ItemData> _heldItems = new ();
         private RecipeData _currentRecipe;
         
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
            
            GameObject retrieved = ObjectPool.Retrieve(itemPrefabToSpawn, transform.position, Quaternion.identity);
            if (retrieved.TryGetComponent(out Item item))
            {
                item.SetUpItem(_currentRecipe.CreatedItem);
                _heldItem =(item);
            }
            else
            {
                return;
            }

            _heldItems.Clear();
            
            _currentRecipe = default;
            
            _isTransoforming = false;
        }
        
        
        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            _heldItems.Add(playerItemHolder.HeldItem.ItemData);
            
            ObjectPool.Store(playerItemHolder.HeldItem.GameObject());
            
            playerItemHolder.RemoveItem();
            
            startTransforming();
        }

        protected override void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            base.pickUpFromItemHolder(playerItemHolder);
            if(_isTransoforming == true) stopTransforming();
        }
        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            this.enabled = (newGameState == GameState.Gameplay) ;
        }

        private void startTransforming()
        {
            if(_isTransoforming) stopTransforming();
            
            _currentRecipe = recipies.GetValidRecipie(_heldItems);
            
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