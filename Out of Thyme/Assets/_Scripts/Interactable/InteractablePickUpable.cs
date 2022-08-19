using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class InteractablePickUpable : Interactable
    {
        #region Variables

        [SerializeField] private Item thisItem;
        
        #endregion

        #region Unity Methods

        //This is here Just so I can enable it/Disable it to tell if the item is pickupable
        private void OnEnable()
        {
            
        }

        #endregion

        #region Methods

        public override void Interact(PlayerItemHolder playerItemHolder)
        {
            if(enabled == false) return; 
            
            playerItemHolder.PickUpItem(thisItem);
            this.enabled = false;
        }

        #endregion
    }
}