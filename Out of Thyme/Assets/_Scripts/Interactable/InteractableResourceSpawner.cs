using System;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using UnityEngine;

namespace TigerFrogGames
{
    [SelectionBase]
    public class InteractableResourceSpawner : Interactable
    {
        #region Variables

        [SerializeField] private GameObject itemPrefabToSpawn;
        [SerializeField] private ItemData itemDataToSpawn;
        
        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (itemDataToSpawn != null)
            {
                transform.name = $"Spawner - {itemDataToSpawn.name}";
            }
        }

        #endregion

        #region Methods

        public override void Interact(PlayerItemHolder playerItemHolder)
        {
            if (itemDataToSpawn == null 
                || itemPrefabToSpawn == null
                || playerItemHolder.HeldItem != null) return;
            
            GameObject retrieved = ObjectPool.Retrieve(itemPrefabToSpawn, transform.position, Quaternion.identity);
            if (retrieved.TryGetComponent(out Item item))
            {
                item.SetUpItem(itemDataToSpawn);
                playerItemHolder.PickUpItem(item);
            }
        }

        #endregion
    }
}