using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using UnityEngine;

namespace TigerFrogGames
{
    public class Item : MonoBehaviour
    {
        #region Variables

        public ItemData ItemData {get; private set;}
        [SerializeField] private GameObject visualRoot;
        [SerializeField] private InteractablePickUpable pickUpable;
        [SerializeField] private Rigidbody rb;
        
        private GameObject _spawnedVisual;
        #endregion

        #region Unity Methods

        #endregion

        #region Methods

        /// <summary>
        /// Sets Up the item to the current item data. 
        /// </summary>
        /// <param name="itemData"></param>
        [ContextMenu("Setup Up Items")]
        public  void SetUpItem(ItemData itemData)
        {
            if(itemData == null) return;
            
            ItemData = itemData;
            this.transform.name = ItemData.name;
            
            if(_spawnedVisual != null) ObjectPool.Store(_spawnedVisual);
            
            _spawnedVisual = ObjectPool.Retrieve(itemData.VisualPrefab, visualRoot.transform.position, Quaternion.identity);
            _spawnedVisual.transform.parent = visualRoot.transform;
        }

        public void PickUp()
        {
            rb.isKinematic = true;
            pickUpable.enabled = false;
        }
        
        public void DropItemToGround()
        {
            rb.isKinematic = false;
            pickUpable.enabled = true;
        }
        
        #endregion
    }
}