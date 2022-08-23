using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteractableThymeAdder : InteratableItemHolder
    {
        #region Variables

        [SerializeField] private EventChannel OnTymeAdded;
        [SerializeField] private ItemData ThymeItemData;
        #endregion

        #region Unity Methods

        #endregion

        #region Methods
    
        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            if(playerItemHolder.HeldItem == null || playerItemHolder.HeldItem.ItemData != ThymeItemData) return;
            
            OnTymeAdded.RaiseEvent();

            var tempItem = playerItemHolder.HeldItem;
            playerItemHolder.RemoveItem();
            ObjectPool.Store(tempItem.GameObject());
            
        }

        
        protected override void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            
        }

        #endregion
    }
}