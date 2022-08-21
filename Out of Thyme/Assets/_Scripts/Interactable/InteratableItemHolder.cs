using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteratableItemHolder : Interactable
    {
        #region Variables

        [SerializeField] private GameObject heldObjectRoot;
        protected Item _heldItem;
        
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        #endregion

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
            _heldItem = playerItemHolder.HeldItem;
            playerItemHolder.PlaceItem(heldObjectRoot.transform);
        }

        protected virtual void pickUpFromItemHolder(PlayerItemHolder playerItemHolder)
        {
            playerItemHolder.PickUpItem(_heldItem);
            _heldItem = null;
        }
        
    }
}