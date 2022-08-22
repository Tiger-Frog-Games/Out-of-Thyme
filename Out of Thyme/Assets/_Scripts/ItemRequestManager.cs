using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TigerFrogGames
{
    public class ItemRequestManager : MonoBehaviour
    {
        #region Variables

        [SerializeField] private EventChannelItemData OnItemSold;

        [SerializeField] private EventChannelRequests OnRequestCreated;
        [SerializeField] private EventChannelRequests OnRequestRemoved;

        [SerializeField] private EventChannelItemData OnFailedItemSold;
        
        [SerializeField] private EventChannelRequests OnRequestCompletedSuccess;
        [SerializeField] private EventChannelRequests OnRequestCompletedFailure;
        
        [SerializeField] private EventChannelRequests OnRequestTimedOut;

        [SerializeField] private float newRequestDelay;
        
        [SerializeField] private Request[] AllPossibleRequests;
        private List<Request> _currentRequests = new ();

        [SerializeField] private int StartingRequests = 3;

        private bool _isGamePaused;
            
        
        #endregion

        #region Unity Methods

        private void Start()
        {
            for (int i = 0; i < StartingRequests; i++)
            {
                AddNewRequest();
            }
        }

        private void Awake()
        {
            GameStateManager.Instance.OnGameStateChanged += GameStateManager_OnGameStateChanged;
            
            OnItemSold.OnEvent += OnItemSoldOnOnEvent;
            OnRequestTimedOut.OnEvent += OnRequestTimedOutOnOnEvent;
        }
        
        private void OnDestroy()
        {
            GameStateManager.Instance.OnGameStateChanged -= GameStateManager_OnGameStateChanged;
            
            OnItemSold.OnEvent -= OnItemSoldOnOnEvent;
            OnRequestTimedOut.OnEvent -= OnRequestTimedOutOnOnEvent;
        }
        
        #endregion

        #region Methods

        
        private void GameStateManager_OnGameStateChanged(GameState newGameState)
        {
            _isGamePaused = (newGameState == GameState.Paused) ;
        }
        
        private void AddNewRequest()
        {
            var requestToAdd = new Request( AllPossibleRequests[Random.Range(0,AllPossibleRequests.Length)].RequestData );
            _currentRequests.Add(requestToAdd);

            OnRequestCreated.RaiseEvent(requestToAdd);
        }

        private void RemoveRequest( Request toRemove )
        {
            _currentRequests.Remove(toRemove);
            OnRequestRemoved.RaiseEvent(toRemove);
            
            StartCoroutine(AddNewRequestAfterDelay());
        }
        
        private void OnItemSoldOnOnEvent(ItemData soldItem)
        {
            var validRequests = new List<Request>();
            foreach (var request in _currentRequests)
            {
                if (request.RequestData.RequiredItem == soldItem)
                {
                    validRequests.Add(request);
                }
            }
            
            //Item that was sold was not matching a request
            if (validRequests.Count == 0)
            {
                OnFailedItemSold.RaiseEvent(soldItem);
                return;
            }
            
            Request lowest = validRequests[0];
            foreach (var VARIABLE in validRequests)
            {
                if (lowest.TimeLeft > VARIABLE.TimeLeft)
                {
                    lowest = VARIABLE;
                }
            }

            RemoveRequest(lowest);
            
            OnRequestCompletedSuccess.RaiseEvent(lowest);
            
        }
        
        private void OnRequestTimedOutOnOnEvent(Request obj)
        {
             RemoveRequest(obj);
             OnRequestCompletedFailure.RaiseEvent(obj);
        }
        
        private IEnumerator  AddNewRequestAfterDelay()
        {
            float timeLeft = newRequestDelay;

            while (timeLeft > 0)
            {
                if (!_isGamePaused)
                {
                    timeLeft -= Time.deltaTime;
                }
                yield return null;
            }
            
            AddNewRequest();
        }
        
        #endregion
    }
}