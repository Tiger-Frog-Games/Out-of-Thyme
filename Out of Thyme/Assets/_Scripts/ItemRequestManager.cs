using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TigerFrogGames
{
    public class ItemRequestManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelItemData OnItemSold;
        
            
        
        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnItemSold.OnEvent += OnItemSoldOnOnEvent;
        }
        
        private void OnDestroy()
        {
            OnItemSold.OnEvent -= OnItemSoldOnOnEvent;
        }

        private void Update()
        {
            
        }

        #endregion

        #region Methods

        private void OnItemSoldOnOnEvent(ItemData obj)
        {
            print($"{obj.name} was sold.");
        }
        
        #endregion
    }
}