using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteratableItemHolder : Interactable
    {
        #region Variables

        [SerializeField] protected GameObject heldObjectRoot;
        
        [SerializeField] private int maxNumberOfItems;
        [SerializeField] protected GameObject[] locationOfHeldItems;
        protected List<Item> _heldItems = new ();
        
        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (locationOfHeldItems != null && maxNumberOfItems != locationOfHeldItems.Length)
            {
                Debug.LogWarning($"Warning {this.gameObject} - Max Numbers of Items and length of Location of Held Game Object must be equal");
            }
        }
        
        #endregion

        #region Methods

        public override void Interact(PlayerItemHolder playerItemHolder)
        {
            
            if (playerItemHolder.HeldItem != null)
            {
                addPlayerItemToItemHolder(playerItemHolder);
            }
            else if (playerItemHolder.HeldItem == null)
            {
                pickUpFromItemHolder(playerItemHolder);
            }

            
        }
        
        protected virtual void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            if(_heldItems.Count  >= maxNumberOfItems) return;
            
            _heldItems.Add(playerItemHolder.HeldItem);
            
            playerItemHolder.PlaceItem(locationOfHeldItems[_heldItems.Count-1].transform);
            
        }

        protected virtual void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            if (_heldItems.Count <= 0) return;
            
            var tempItem = _heldItems[_heldItems.Count - 1];
            _heldItems.RemoveAt(_heldItems.Count - 1);
                
            playerItemHolder.PickUpItem(tempItem);
        }
        
        #endregion
        
    }
}