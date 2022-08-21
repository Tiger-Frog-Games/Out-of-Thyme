using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerItemHolder : MonoBehaviour
    {
        #region Variables

        [SerializeField] private PlayerInteractor playerInteractor;
        [SerializeField] private GameObject heldItemRoot;
        [SerializeField] private GameObject dropItemLocation;

        public Item HeldItem { get; private set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public void PickUpItem(Item newHeldItem)
        {
            if(newHeldItem == null) return;
            
            HeldItem = newHeldItem;
            
            HeldItem.transform.parent = heldItemRoot.transform;
            HeldItem.transform.localPosition = Vector3.zero + HeldItem.ItemData.OffSetHeldOnPlayerHead;
            HeldItem.PickUp();
            
        }

        public void DropItem()
        {
            //check to see if there is an interactor to drop onto
            var itemHolder = playerInteractor.GetInteratableItemHolderIsHovered();
            if (itemHolder != null)
            {
                itemHolder.Interact(this);
                return;
            }
            
            
            //drop item to ground
            HeldItem.transform.parent = null;
            HeldItem.transform.position = dropItemLocation.transform.position;
            HeldItem.DropItemToGround();
            
            HeldItem = null;
        }

        public void PlaceItem( Transform dropTransform )
        {
            HeldItem.transform.parent = dropTransform;
            HeldItem.transform.localPosition = Vector3.zero + HeldItem.ItemData.OffsetHeldOnInteractableItemHolder ;
            
            HeldItem = null;
        }

        public void RemoveItem()
        {
            HeldItem = null;
        }
        
        public string UseItem()
        {
            if (HeldItem == null) return"";

            if(HeldItem is ItemUsable usable)
            {
                return usable.UseItem();
            }

            return "";
        }
        
        #endregion
    }
}