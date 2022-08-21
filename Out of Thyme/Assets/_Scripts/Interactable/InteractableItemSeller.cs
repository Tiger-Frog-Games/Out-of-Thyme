using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using Unity.VisualScripting;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteractableItemSeller : InteratableItemHolder
    {
        #region Variables

        [SerializeField] private EventChannelItemData OnSellItem;
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods
    
        protected override void addPlayerItemToItemHolder(PlayerItemHolder playerItemHolder)
        {
            if(playerItemHolder.HeldItem == null) return;
            
            OnSellItem.RaiseEvent(playerItemHolder.HeldItem.ItemData);

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