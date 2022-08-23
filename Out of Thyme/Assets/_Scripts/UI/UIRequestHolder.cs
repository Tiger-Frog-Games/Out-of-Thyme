using System;
using System.Collections;
using System.Collections.Generic;
using FirstGearGames.FastObjectPool;
using UnityEngine;

namespace TigerFrogGames
{
    public class UIRequestHolder : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject RequestUIPrefab;

        [SerializeField] private GameObject UIPanelRoot;
        
        [SerializeField] private EventChannelRequests OnRequestAdded;
        [SerializeField] private EventChannelRequests OnRequestRemoved;

        private Dictionary<Request, UIRequest> _uiRequests = new();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            OnRequestAdded.OnEvent += OnRequestAddedOnOnEvent;
            OnRequestRemoved.OnEvent += OnRequestRemovedOnOnEvent;
        }
        
        private void OnDestroy()
        {
            OnRequestAdded.OnEvent -= OnRequestAddedOnOnEvent;
            OnRequestRemoved.OnEvent -= OnRequestRemovedOnOnEvent;
        }

        #endregion

        #region Methods

        private void OnRequestAddedOnOnEvent(Request obj)
        {
            GameObject temp = ObjectPool.Retrieve(RequestUIPrefab, UIPanelRoot.transform.position, Quaternion.identity);
            temp.transform.SetParent(UIPanelRoot.transform);
            
            if (temp.TryGetComponent(out UIRequest requestUI))
            {
                
                requestUI.setUpRequest(obj);
                _uiRequests.Add(obj,requestUI);
            }
        }
        
        private void OnRequestRemovedOnOnEvent(Request obj)
        {
            if (_uiRequests.TryGetValue(obj, out UIRequest uiRequest))
            {
                _uiRequests.Remove(obj);
                ObjectPool.Store(uiRequest.gameObject);
            }
        }
        
        #endregion
    }
}