using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class PlayerItemHolder : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject heldItemRoot;
        [SerializeField] private GameObject dropItemLocation;
        
        public Item HeldItem { get; private set; }

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        public void PickUpItem(Item newHeldItem)
        {
            HeldItem = newHeldItem;
            
            HeldItem.transform.parent = heldItemRoot.transform;
            HeldItem.transform.localPosition = Vector3.zero + HeldItem.ItemData.HeldOffset;
            HeldItem.PickUp();
            
        }

        public void DropItem()
        {
            HeldItem.transform.parent = null;
            HeldItem.transform.position = dropItemLocation.transform.position;
            HeldItem.DropItemToGround();
            
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