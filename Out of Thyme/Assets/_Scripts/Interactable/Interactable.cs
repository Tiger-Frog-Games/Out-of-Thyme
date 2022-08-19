using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public abstract class Interactable : MonoBehaviour
    {
        #region Variables

        #endregion

        #region Unity Methods

        
        
        #endregion

        #region Methods

        public abstract void Interact(PlayerItemHolder playerItemHolder);

        #endregion
    }
}