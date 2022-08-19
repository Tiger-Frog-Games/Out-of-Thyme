using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace TigerFrogGames
{
    public class ItemUsable : Item
    {
        #region Variables

        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        /// <summary>
        /// Uses the held item if it is usable
        /// </summary>
        /// <returns>The animation that the player uses to use an item</returns>
        public string UseItem()
        {
            if (ItemData!= null && ItemData is ItemDataUsable usable)
            {
                return usable.usableAnimation;
            }

            return "";
        }
        
        #endregion
    }
}